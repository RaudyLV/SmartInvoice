using System.Net;
using System.Text.Json;
using FluentValidation;
using SmartInvoice.Application.Exceptions;

namespace SmartInvoice.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError($"[ErrorMiddleware] Response HasStarted: {context.Response.HasStarted}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    var errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    result = JsonSerializer.Serialize(errors);
                    break;

                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                    
                case UnAuthorizedException _:
                    code = HttpStatusCode.Unauthorized;
                    break;

                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new
                {
                    error = ex.Message
                });
            }

            await context.Response.WriteAsync(result);
        }
    }
}