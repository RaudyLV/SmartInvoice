using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Features.Products.Queries;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.Products;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IBaseRepository<Product> _baseRepository;

        public ProductServices(IBaseRepository<Product> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddProduct(Product product)
        {
            await _baseRepository.AddAsync(product);
            await _baseRepository.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            await _baseRepository.DeleteAsync(product);
            await _baseRepository.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> ProductsWithFilterAsync(GetAllProductsQuery query)
        {
            var products = await _baseRepository.ListAsync(new ProductsWithFilterSpec(
                searchTerm: query.SearchTerm,
                minPrice: query.MinPrice,
                maxPrice: query.MaxPrice,
                pageNumber: query.PageNumber,
                pageSize: query.PageSize, sortBy: query.SortBy,
                sortDescending: query.SortDescending
            ));
            
            if (products == null || !products.Any())
            {
                throw new NotFoundException("No products available");
            }

            return products;
        }
        public async Task<int> CountAsync(string searchTerm)
        {
            int totalCount = await _baseRepository.CountAsync(new CountProductsSpec(searchTerm));

            return totalCount;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _baseRepository.FirstOrDefaultAsync(new GetProductSpec(id));
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            return product;
        }

        public async Task<ProductDto> VerifyExistingProduct(string productName)
        {
            var existinProduct = await _baseRepository.FirstOrDefaultAsync(new GetProductByNameSpec(productName));
            if (existinProduct != null)
                throw new BadRequestException("Product already exists.");

            return existinProduct!;
        }

        public async Task<List<ProductDto>> GetProductsByIds(List<int> ids)
        {
            return await _baseRepository.ListAsync(new GetProductsByIdsSpec(ids));
        }

        public async Task ReduceStock(int productId, int quantity)
        {
            var product = await _baseRepository.GetByIdAsync(productId);
            if (product == null)
                throw new NotFoundException("Product not found");

            if (product.Stock < quantity)
                throw new BadRequestException("Insufficient stock.");

           
                product.Stock -= quantity;

                await UpdateProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _baseRepository.UpdateAsync(product);
            await _baseRepository.SaveChangesAsync();
        }

    }
}