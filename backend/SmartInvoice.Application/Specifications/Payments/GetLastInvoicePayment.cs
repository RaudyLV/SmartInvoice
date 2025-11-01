using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Payments
{
    public class GetLastInvoicePayment : Specification<Payment, PaymentDto>
    {
        public GetLastInvoicePayment(int invoiceId)
        {
            Query.Where(x => x.InvoiceId == invoiceId)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    InvoiceNumber = p.Invoice.InvoiceNumber,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Method = p.Method.ToString()
                });
        }
    }
}