using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Clients.Queries
{
    public class GetClientInvoicesByFilterQuery : IRequest<Response<PagedList<InvoiceDto>>>
    {
        public string Name { get; set; } = null!;
        public DateTime? IssuedDate { get; set; } = null;
        public DateTime? DueDate { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public Status? Status { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = false;
    }

    public class GetClientInvoicesByFilterQueryHandler
    : IRequestHandler<GetClientInvoicesByFilterQuery, Response<PagedList<InvoiceDto>>>
    {
        private readonly IInvoiceServices _invoiceServices;
        public GetClientInvoicesByFilterQueryHandler(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        public async Task<Response<PagedList<InvoiceDto>>> Handle(GetClientInvoicesByFilterQuery request, CancellationToken cancellationToken)
        {
            var clientInvoicesDto = await _invoiceServices.GetClientInvoicesByFilter(request);

            int totalCount = await _invoiceServices.CountAsync(request.Name);

            var pagedList = PagedList<InvoiceDto>.Create(
                clientInvoicesDto,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
            return new Response<PagedList<InvoiceDto>>(pagedList);
        }
    }
}