using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Clients
{
    public class GetAllClientsSpec : Specification<Client, ClientDto>
    {
        public GetAllClientsSpec()
        {
            Query
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