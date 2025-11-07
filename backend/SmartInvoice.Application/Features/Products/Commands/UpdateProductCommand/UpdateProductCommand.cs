using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<Response<ProductDto>>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductDto>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;
        private readonly ICacheServices _cacheServices;
        public UpdateProductCommandHandler(IProductServices productServices, IMapper mapper, ICacheServices cacheServices)
        {
            _productServices = productServices;
            _mapper = mapper;
            _cacheServices = cacheServices;
        }
        
        public async Task<Response<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var existingProductDto = await _productServices.GetProduct(request.ProductId)
                    ?? throw new NotFoundException("Product not found");
                    
            if (!string.IsNullOrEmpty(request.Name) && request.Name != existingProductDto.Name)
                await _productServices.VerifyExistingProduct(request.Name!);

            var existingProduct = _mapper.Map<Product>(existingProductDto);

            _mapper.Map(request, existingProduct);

            await _productServices.UpdateProduct(existingProduct);

            await _cacheServices.RemoveByPrefixAsync("products_list");

            var updatedProductDto = _mapper.Map<ProductDto>(existingProduct);

            return new Response<ProductDto>(updatedProductDto, "Product updated successfully");
        }
    }
}
