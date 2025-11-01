using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Clients.Commands.CreateClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.DeleteClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.UpdateClientCommand;
using SmartInvoice.Application.Features.Clients.Queries;

namespace SmartInvoice.API.Controllers
{
    [Authorize]
    public class ClientsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetClientsAsync()
        {
            return Ok(await Mediator!.Send(new GetAllClientsQuery()));
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindClientByQueryAsync([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
                return BadRequest("Search query is required");

            return Ok(await Mediator!.Send(new FindClientByQuery(q)));
        }

        [HttpGet("{name}/invoices")]
        public async Task<IActionResult> GetClientInvoicesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Client name is required");

            return Ok(await Mediator!.Send(new GetClientInvoicesByNameQuery(name)));
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