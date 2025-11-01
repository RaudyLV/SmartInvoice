using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Features.Products.Commands.CreateProductCommand;
using SmartInvoice.Application.Features.Products.Commands.DeleteProductCommand;
using SmartInvoice.Application.Features.Products.Commands.UpdateProductCommand;
using SmartInvoice.Application.Features.Products.Queries;
namespace SmartInvoice.API.Controllers
{
    [Authorize(Roles = "User")]
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            return Ok(await Mediator!.Send(new GetAllProductsQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            return Ok(await Mediator!.Send(new GetProductByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>DeleteProductAsync(int id)
        {
            return Ok(await Mediator!.Send(new DeleteProductCommand(id)));
        }
    }
}