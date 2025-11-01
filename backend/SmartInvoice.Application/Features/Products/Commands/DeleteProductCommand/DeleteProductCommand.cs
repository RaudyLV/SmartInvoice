using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<Response<ProductDto>>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<ProductDto>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;
        public DeleteProductCommandHandler(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productDto = await _productServices.GetProduct(request.Id);

            var product = _mapper.Map<Product>(productDto);

            await _productServices.DeleteProduct(product);

            return new Response<ProductDto>(productDto, "Product deleted successfully");
        }
    }
}