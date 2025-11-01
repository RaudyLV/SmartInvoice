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

        public CancelInvoiceCommandHandler(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;

        }

        public async Task<Response<string>> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceServices.CancelInvoice(request.InvoiceId);

            return new Response<string>("Invoice cancelled successfully");
        }
    }
}