using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Clients
{
    public class GetClientByEmailSpec : Specification<Client, ClientDto>
    {
        public GetClientByEmailSpec(string email)
        {
            Query.Where(x => x.Email == email)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Email = c.Email
                });
        }
    }
}