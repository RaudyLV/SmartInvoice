using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Clients.Commands.CreateClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.DeleteClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.UpdateClientCommand;
using SmartInvoice.Application.Features.Clients.Queries;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.API.Controllers
{
    [Authorize]
    public class ClientsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetClientsAsync(
            [FromQuery] string? q,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true
        )
        {
            var query = new GetAllClientsQuery
            {
                SearchTerm = q!,
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


        [HttpGet("{name}/invoices")]
        public async Task<IActionResult> GetClientInvoicesByName(
            string name,
            [FromQuery] DateTime? issuedDate,
            [FromQuery] DateTime? dueDate,
            [FromQuery] int? minPrice,
            [FromQuery] int? maxPrice,
            [FromQuery] Status? status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true
        )
        {
            var query = new GetClientInvoicesByFilterQuery
            {
                Name = name,
                IssuedDate = issuedDate,
                DueDate = dueDate,
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
                result.Data.HasNextPage
            }));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientCommand command)
        {
            return Ok(await Mediator!.Send(command));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateClientAsync(int id, [FromBody] UpdateClientCommand command)
        {
            return Ok(await Mediator!.Send(command));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            return Ok(await Mediator!.Send(new DeleteClientCommand(id)));
        }

    }
}