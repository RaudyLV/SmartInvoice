using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Clients
{
    public class FindClientByQuerySpec : Specification<Client, ClientDto>
    {
        public FindClientByQuerySpec(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                Query
                    .Search(x => x.Email, "%" + query + "%")
                    .Search(x => x.Name, "%" + query + "%")
                    .Select(x => new ClientDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Address = x.Address,
                        Phone = x.Phone,
                        CreatedAt = x.CreatedAt,
                    });
            }
        }
    }
}