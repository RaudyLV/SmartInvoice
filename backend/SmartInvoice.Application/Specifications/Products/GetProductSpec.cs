
using Ardalis.Specification;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Products
{
    public class GetProductSpec : Specification<Product, ProductDto>
    {
        public GetProductSpec(int id)
        {
            Query.Where(x => x.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Stock = p.Stock,
                    Price = p.Price,
                    TaxRate = p.TaxRate,
                    CreatedAt = p.CreatedAt
                });
        }   
    }
}