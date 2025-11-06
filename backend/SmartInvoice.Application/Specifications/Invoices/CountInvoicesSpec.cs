using Ardalis.Specification;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Invoices
{
    public class CountInvoicesSpec : Specification<Invoice>
    {
        public CountInvoicesSpec(string query = null!)
        {
            if(!string.IsNullOrEmpty(query))
            {
                Query.Search(x => x.Client.Name, $"%{query}%")
                    .Search(x => x.InvoiceNumber, $"%{query}%");
            }
        }
    }
}