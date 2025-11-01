using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Clients.Queries
{
    public class GetClientInvoicesByNameQuery : IRequest<Response<List<InvoiceDto>>>
    {
        public string ClientName { get; set; }

        public GetClientInvoicesByNameQuery(string clientName)
        {
            ClientName = clientName;
        }
    }

    public class GetClientInvoicesByNameQueryHandler : IRequestHandler<GetClientInvoicesByNameQuery, Response<List<InvoiceDto>>>
    {
        private readonly IInvoiceServices _invoiceServices;
        public GetClientInvoicesByNameQueryHandler(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        public async Task<Response<List<InvoiceDto>>> Handle(GetClientInvoicesByNameQuery request, CancellationToken cancellationToken)
        {
            var clientInvoices = await _invoiceServices.GetClientInvoicesByName(request.ClientName);
            
            return new Response<List<InvoiceDto>>(clientInvoices);
        }
    }
}