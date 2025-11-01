using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Invoices.Queries
{
    public record GetActiveInvoicesQuery : IRequest<Response<List<InvoiceDto>>>;

    public class GetActiveInvoicesQueryHandler : IRequestHandler<GetActiveInvoicesQuery, Response<List<InvoiceDto>>>
    {
        private readonly IInvoiceServices _invoiceServices;

        public GetActiveInvoicesQueryHandler(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        public async Task<Response<List<InvoiceDto>>> Handle(GetActiveInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceServices.GetActivesInvoices();

            return new Response<List<InvoiceDto>>(invoices);
        }
    }
}