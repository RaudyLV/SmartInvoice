using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<Response<ProductDto>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public CreateProductCommand(string name, decimal price, int stock, string? description = null)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }


    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDto>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productServices.VerifyExistingProduct(request.Name);

            var product = _mapper.Map<Product>(request);

            product.Stock = request.Stock > 0 ? request.Stock : 0;
            product.TaxRate = 0.18m;
            product.CreatedAt = DateTime.UtcNow;
            product.Description = !string.IsNullOrEmpty(request.Description) 
                                    ? request.Description 
                                    : "No description available";

            await _productServices.AddProduct(product);

            var productDto = _mapper.Map<ProductDto>(product);

            return new Response<ProductDto>(productDto, "Product created successfully");
        }
    }
}