using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IClientServices
    {
        Task<List<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> FindClientByQuery(string query); //Filtro por email, empresa, nombre
        Task<ClientDto> GetClientByEmail(string email); 
        Task<Client> GetClientById(int id); 
        Task AddClient(Client client);
        Task DeleteClient(Client client);
        Task UpdateClient(Client client);
    }
}