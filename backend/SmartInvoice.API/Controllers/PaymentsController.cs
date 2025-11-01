using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Payments.Commands;
using SmartInvoice.Application.Features.Payments.Queries;

namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPaymentsAsync()
        {
            return Ok(await Mediator!.Send(new GetAllPaymentsQuery()));
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