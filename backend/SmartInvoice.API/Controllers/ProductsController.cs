using System.Text.Json;
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
        public async Task<IActionResult> GetAllProductsAsync(
            [FromQuery] string? search,
            [FromQuery] int? minPrice,
            [FromQuery] int? maxPrice,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true
        )
        {
            var query = new GetAllProductsQuery
            {
                SearchTerm = search!,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
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