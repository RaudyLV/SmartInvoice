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
        private readonly ICacheServices _cacheServices;
        public DeleteProductCommandHandler(IProductServices productServices, IMapper mapper, ICacheServices cacheServices)
        {
            _productServices = productServices;
            _mapper = mapper;
            _cacheServices = cacheServices;
        }

        public async Task<Response<ProductDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productDto = await _productServices.GetProduct(request.Id);

            var product = _mapper.Map<Product>(productDto);

            await _productServices.DeleteProduct(product);

            await _cacheServices.RemoveByPrefixAsync("products_list", cancellationToken);

            return new Response<ProductDto>(productDto, "Product deleted successfully");
        }
    }
}