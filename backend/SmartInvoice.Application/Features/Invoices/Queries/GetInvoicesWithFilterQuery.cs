using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Invoices.Queries
{
    public record GetInvoicesWithFilterQuery : IRequest<Response<PagedList<InvoiceDto>>>
    {
        public string SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Status? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = false;
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