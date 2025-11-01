using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Payments
{
    public class GetAllPaymentsSpec : Specification<Payment, PaymentDto>
    {
        public GetAllPaymentsSpec()
        {
            Query
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