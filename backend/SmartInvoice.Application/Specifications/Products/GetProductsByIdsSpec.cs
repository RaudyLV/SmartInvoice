

using Ardalis.Specification;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Products
{
    public class GetProductsByIdsSpec : Specification<Product, ProductDto>
    {
        public GetProductsByIdsSpec(List<int> ids)
        {
            Query.Where(p => ids.Contains(p.Id))
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