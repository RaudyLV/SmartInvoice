using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Invoices.Commands.CancelInvoiceCommand;
using SmartInvoice.Application.Features.Invoices.Commands.CreateInvoicesCommand;
using SmartInvoice.Application.Features.Invoices.Queries;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "User")]
    public class InvoicesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetInvoicesAsync(
            [FromQuery] string? search,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] Status? status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true
        )
        {
            var query = new GetInvoicesWithFilterQuery
            {
                SearchTerm = search!,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Status = status,
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
                result.Data.HasNextPage,
            }));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoiceAsync([FromBody] CreateInvoiceCommand command)
        {
            return Ok(await Mediator!.Send(command));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelInvoiceAsync(int id)
        {
            return Ok(await Mediator!.Send(new CancelInvoiceCommand(id)));
        }
    }
}