using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<Response<PagedList<ProductDto>>>
    {
        public string SearchTerm { get; set; } 
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = false;
    }


    public class GetAllProductsQueryHandler
    : IRequestHandler<GetAllProductsQuery, Response<PagedList<ProductDto>>>
    {
        private readonly IProductServices _productServices;

        public GetAllProductsQueryHandler(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<Response<PagedList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productServices.ProductsWithFilterAsync(request);

            int totalCount = await _productServices.CountAsync(request.SearchTerm);

            var pagedList = PagedList<ProductDto>.Create(
                products,
                totalCount,
                request.PageNumber,
                request.PageSize
            );

            return new Response<PagedList<ProductDto>>(pagedList);
        }
    }

}