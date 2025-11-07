using System.Security.Cryptography;
using System.Text;
using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Invoices.Queries
{
    public record GetInvoicesWithFilterQuery : IRequest<Response<PagedList<InvoiceDto>>>,
    ICacheableQuery
    {
        public string SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Status? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = false;

        public string CacheKey
        {
            get
            {
                var keyComponents = $"{SearchTerm}|{MinPrice}|{MaxPrice}|{Status}" +
                                    $"{PageNumber}|{PageSize}|{SortBy}|{SortDescending}";

                var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyComponents));
                var hash = Convert.ToBase64String(hashBytes);

                return $"invoices_list_{hash}";
            }
        }

        public TimeSpan? CacheDuration => TimeSpan.FromMinutes(5);
    }

    public class GetActiveInvoicesQueryHandler : IRequestHandler<GetInvoicesWithFilterQuery, Response<PagedList<InvoiceDto>>>
    {
        private readonly IInvoiceServices _invoiceServices;

        public GetActiveInvoicesQueryHandler(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        public async Task<Response<PagedList<InvoiceDto>>> Handle(GetInvoicesWithFilterQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceServices.InvoicesWithFilterAsync(request);

            int totalCount = await _invoiceServices.CountAsync(request.SearchTerm);

            var pagedList = PagedList<InvoiceDto>.Create(
                invoices,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
            
            return new Response<PagedList<InvoiceDto>>(pagedList);
        }
    }
}