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

        public UpdateProductCommandHandler(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var existingProductDto = await _productServices.GetProduct(request.ProductId);
            if (existingProductDto == null)
                throw new NotFoundException("Product not found");

            if (!string.IsNullOrEmpty(request.Name) && request.Name != existingProductDto.Name)
                await _productServices.VerifyExistingProduct(request.Name!);

            var existingProduct = _mapper.Map<Product>(existingProductDto);

            _mapper.Map(request, existingProduct);

            await _productServices.UpdateProduct(existingProduct);

            var updatedProductDto = _mapper.Map<ProductDto>(existingProduct);

            return new Response<ProductDto>(updatedProductDto, "Product updated successfully");
        }
    }
}
