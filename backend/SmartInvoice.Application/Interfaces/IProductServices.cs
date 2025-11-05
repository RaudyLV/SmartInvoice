using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Features.Products.Queries;

namespace SmartInvoice.Application.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductDto>> GetProductsByIds(List<int> ids);
        Task<List<ProductDto>> ProductsWithFilterAsync(GetAllProductsQuery query);
        Task<int> CountAsync(string searchTerm);
        Task<ProductDto> VerifyExistingProduct(string productName);
        Task<ProductDto> GetProduct(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task ReduceStock(int productId, int quantity);
    }
}