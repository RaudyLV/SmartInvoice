using Ardalis.Specification;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Products
{
    public class GetProductByNameSpec : Specification<Product, ProductDto>
    {
        public GetProductByNameSpec(string name)
        {
            string nameSearch = name.Trim();   
            Query.Search(x => x.Name, "%" + nameSearch + "%")
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    TaxRate = p.TaxRate,
                    CreatedAt = p.CreatedAt
                });   
        }
    }
}