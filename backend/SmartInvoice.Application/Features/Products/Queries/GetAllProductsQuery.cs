using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Response<List<ProductDto>>>;


    public class GetAllProductsQueryHandler
    : IRequestHandler<GetAllProductsQuery, Response<List<ProductDto>>>
    {
        private readonly IProductServices _productServices;

        public GetAllProductsQueryHandler(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<Response<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productServices.GetAllProducts();

            return new Response<List<ProductDto>>(products);
        }
    }

}