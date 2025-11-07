using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<ClientDto>>
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<ClientDto>>
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICacheServices _cacheServices;
        public UpdateClientCommandHandler(IClientServices clientServices, IMapper mapper, ICacheServices cacheServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _cacheServices = cacheServices;
        }

        public async Task<Response<ClientDto>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientServices.GetClientById(request.ClientId);
            if (client == null)
                throw new NotFoundException("Client not found");

            _mapper.Map(request, client);
            await _clientServices.UpdateClient(client);

            await _cacheServices.RemoveByPrefixAsync("clients_list", cancellationToken);

            var clientDto = _mapper.Map<ClientDto>(client);

            return new Response<ClientDto>(clientDto, "Client updated successfully");
        }
    }
}