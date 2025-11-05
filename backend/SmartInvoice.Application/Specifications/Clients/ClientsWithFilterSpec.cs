using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Clients
{
    public class ClientsWithFilterSpec : Specification<Client, ClientDto>
    {
        public ClientsWithFilterSpec(
            string searchTerm = null!,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool sortDescending = false)
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);

            Query.Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(c => new ClientDto
                   {
                       Id = c.Id,
                       Name = c.Name,
                       Email = c.Email,
                       Phone = c.Phone,
                       Address = c.Address,
                       CreatedAt = c.CreatedAt
                   });
                    
            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Name, $"%{normalizedSearch}%")
                    .Search(x => x.Email, $"%{normalizedSearch}%");
            }

            if(sortDescending)
            {
                switch(sortBy?.ToLower())
                {
                    case "Email":
                        Query.OrderByDescending(x => x.Email);
                        break;

                    default:
                        Query.OrderByDescending(x => x.Name);
                        break;
                }
            }
            else
            {
                switch(sortBy?.ToLower())
                {
                    case "Email":
                        Query.OrderBy(x => x.Email);
                        break;
                    default:
                        Query.OrderBy(x => x.Name);
                        break;
                }
            }
        }
    }
}