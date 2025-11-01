
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Response<ProductDto>>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, Response<ProductDto>>
    {
        private readonly IProductServices _productServices;
        public GetProductByIdQueryHandler(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<Response<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productServices.GetProduct(request.Id);

            return new Response<ProductDto>(product);
        }
    }
}