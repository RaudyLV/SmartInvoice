using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Invoices.Commands.CancelInvoiceCommand;
using SmartInvoice.Application.Features.Invoices.Commands.CreateInvoicesCommand;
using SmartInvoice.Application.Features.Invoices.Queries;

namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "User")]
    public class InvoicesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GeActiveInvoicesAsync()
        {
            return Ok(await Mediator!.Send(new GetActiveInvoicesQuery()));
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