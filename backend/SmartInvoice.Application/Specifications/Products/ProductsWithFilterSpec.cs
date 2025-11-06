using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Products
{
    public class ProductsWithFilterSpec : Specification<Product, ProductDto>
    {
        public ProductsWithFilterSpec(
            string searchTerm = null!,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "CreatedAt",
            bool sortDescending = false
        )
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Name, $"%{normalizedSearch}%")
                    .Search(x => x.Description, $"%{normalizedSearch}%");
            }
            
            if(minPrice.HasValue && maxPrice.HasValue)
            {
                Query.Where(x => x.Price >= minPrice.Value && x.Price <= maxPrice.Value);
            }
            else if(minPrice.HasValue)
            {
                Query.Where(x => x.Price <= minPrice.Value);
            }
            else if (maxPrice.HasValue)
            {
                Query.Where(x => x.Price >= maxPrice.Value);
            }

            if (sortDescending)
            {
                switch (sortBy?.ToLower())
                {
                    case "Name":
                        Query.OrderByDescending(x => x.Name);
                        break;

                    case "Price":
                        Query.OrderByDescending(x => x.Price);
                        break;

                    default:
                        Query.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }
            else
            {
                switch (sortBy?.ToLower())
                {
                    case "Name":
                        Query.OrderBy(x => x.Name);
                        break;

                    case "Price":
                        Query.OrderBy(x => x.Price);
                        break;

                    default:
                        Query.OrderBy(x => x.CreatedAt);
                        break;
                }
                
            
            }
        }
    }
}