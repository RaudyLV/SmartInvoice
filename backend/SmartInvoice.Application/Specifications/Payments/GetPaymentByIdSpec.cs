
using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Payments
{
    public class GetPaymentByIdSpec : Specification<Payment, PaymentDto>
    {
        public GetPaymentByIdSpec(int id)
        {
           Query.Where(p => p.Id == id)
                .Select(x => new PaymentDto
                {
                    Id = x.Id,
                    InvoiceNumber = x.Invoice.InvoiceNumber,
                    Amount = x.Amount,
                    Method = x.Method.ToString(),
                    PaymentDate = x.PaymentDate
                });
        }
    }
}