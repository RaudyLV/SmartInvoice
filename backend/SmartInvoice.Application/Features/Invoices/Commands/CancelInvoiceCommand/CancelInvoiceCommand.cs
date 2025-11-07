using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Invoices.Commands.CancelInvoiceCommand
{
    public class CancelInvoiceCommand : IRequest<Response<string>>
    {
        public int InvoiceId;

        public CancelInvoiceCommand(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, Response<string>>
    {
        private IInvoiceServices _invoiceServices;
        private readonly ICacheServices _cacheServices;
        public CancelInvoiceCommandHandler(IInvoiceServices invoiceServices, ICacheServices cacheServices)
        {
            _invoiceServices = invoiceServices;
            _cacheServices = cacheServices;
        }

        public async Task<Response<string>> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceServices.CancelInvoice(request.InvoiceId);
            
            await _cacheServices.RemoveByPrefixAsync("invoices_list", cancellationToken);

            return new Response<string>("Invoice cancelled successfully");
        }
    }
}