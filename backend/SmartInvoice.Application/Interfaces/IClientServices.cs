using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Features.Clients.Queries;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IClientServices
    {
        Task<List<ClientDto>> GetAllClientsAsync(GetAllClientsQuery query);
        Task<int> CountAsync(string searchTerm);
        Task<ClientDto> GetClientByEmail(string email);
        Task<Client> GetClientById(int id);
        Task AddClient(Client client);
        Task DeleteClient(Client client);
        Task UpdateClient(Client client);
    }
}