
using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<ClientDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CreateClientCommand(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<ClientDto>>
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICacheServices _cacheServices;
        public CreateClientCommandHandler(IClientServices clientServices, IMapper mapper, ICacheServices cacheServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _cacheServices = cacheServices;
        }

        public async Task<Response<ClientDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Client>(request);
            client.CreatedAt = DateTime.UtcNow;
            client.Address = !string.IsNullOrEmpty(request.Address) ?
                            request.Address
                            : "No address available";

            await _clientServices.AddClient(client);

            await _cacheServices.RemoveByPrefixAsync("clients_list", cancellationToken);

            var clientDto = _mapper.Map<ClientDto>(client);

            return new Response<ClientDto>(clientDto, "Client created successfully");
        }
    }
}