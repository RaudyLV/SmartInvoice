using FluentValidation;
using Ardalis.Specification;
using MediatR;

namespace SmartInvoice.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validatots;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validatots)
        {
            _validatots = validatots ?? throw new ArgumentNullException(nameof(validatots));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validatots.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validatots.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
                

            }

            return await next();
        }
    }
}