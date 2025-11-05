using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Payments.Commands;
using SmartInvoice.Application.Features.Payments.Queries;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPaymentsAsync(
            [FromQuery] string? search,
            [FromQuery] int? minAmount,
            [FromQuery] int? maxAmount,
            [FromQuery] PaymentMethod? method,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "PaymentDate",
            [FromQuery] bool sortDescending = true
        )
        {
            var query = new GetAllPaymentsQuery
            {
                SearchTerm = search!,
                MinAmount = minAmount,
                MaxAmount = maxAmount,
                Method = method,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var result = await Mediator!.Send(query);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new
            {
                result.Data.TotalCount,
                result.Data.PageNumber,
                result.Data.PageSize,
                result.Data.TotalPages,
                result.Data.HasPreviousPage,
                result.Data.HasNextPage
            }));

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentAsync(int id)
        {
            return Ok(await Mediator!.Send(new GetPaymentByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] PayInvoiceCommand command)
        {
            return Ok(await Mediator!.Send(command));
        }
    }
}