using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.Clients;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IBaseRepository<Client> _repository;
        public ClientServices(IBaseRepository<Client> repository)
        {
            _repository = repository;
        }

        public async Task AddClient(Client client)
        {
            await GetClientByEmail(client.Email);

            await _repository.AddAsync(client);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteClient(Client client)
        {
            await _repository.DeleteAsync(client);
            await _repository.SaveChangesAsync();
        }

        public async Task<ClientDto> FindClientByQuery(string query)
        {
            var client = await _repository.FirstOrDefaultAsync(new FindClientByQuerySpec(query));
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            return client;
        }
        public async Task<ClientDto> GetClientByEmail(string email)
        {
            var existingClient = await _repository.FirstOrDefaultAsync(new GetClientByEmailSpec(email));
            if (existingClient != null)
            {
                throw new BadRequestException("The email is already in use");
            }
            return existingClient!;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }
            return client;
        }

        public async Task UpdateClient(Client client)
        {
            await GetClientByEmail(client.Email);

            await _repository.UpdateAsync(client);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _repository.ListAsync(new GetAllClientsSpec());
            if (!clients.Any())
                throw new NotFoundException("No active clients found");

            return clients;   
        }
    }
}