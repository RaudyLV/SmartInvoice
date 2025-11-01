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
        public async Task<IActionResult> GetAllUsersAsyns()
        {
            return Ok(await Mediator!.Send(new GetAllUsersQuery()));
        }

        [HttpGet("{id}", Name = "GetUserById")]
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

        