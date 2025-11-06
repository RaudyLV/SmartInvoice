
using Ardalis.Specification;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Clients
{
    public class ClientCountSpec : Specification<Client>
    {
        public ClientCountSpec(string searchTerm = null!)
        {
            if(!string.IsNullOrEmpty(searchTerm))
            {
                Query.Search(x => x.Name, $"%{searchTerm}%")
                    .Search(x => x.Email, $"%{searchTerm}%");
            }
        }
    }
}