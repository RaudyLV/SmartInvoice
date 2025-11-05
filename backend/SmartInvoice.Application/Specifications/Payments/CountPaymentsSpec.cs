using Ardalis.Specification;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Payments
{
    public class CountPaymentsSpec : Specification<Payment>
    {
        public CountPaymentsSpec(string searchTerm)
        {
            if(!string.IsNullOrEmpty(searchTerm))
            {
                Query.Search(x => x.Invoice.InvoiceNumber, $"%{searchTerm}%");
            }
        }
    }
}