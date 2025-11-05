using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Users.Commands.DeleteUserCommand;
using SmartInvoice.Application.Features.Users.Commands.UpdateUserCommand;
using SmartInvoice.Application.Features.Users.Queries;

namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(
            [FromQuery] string? search = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false
        )
        {
            var query = new GetUsersWithFilterQuery
            {
                SearchTerm = search!,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy!,
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            return Ok(await Mediator!.Send(new GetUserByIdQuery
            {
                Id = id
            }));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserCommand request)
        {
            return Ok(await Mediator!.Send(new UpdateUserCommand
            {
                Id = id,
                Email = request.Email,
                UserName = request.UserName
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            return Ok(await Mediator!.Send(new DeleteUserCommand
            {
                Id = id
            }));
        }
    }
}

        