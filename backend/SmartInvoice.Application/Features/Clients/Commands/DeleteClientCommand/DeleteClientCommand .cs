using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Clients.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<ClientDto>>
    {
        public int ClientId { get; set; }

        public DeleteClientCommand(int clientId)
        {
            ClientId = clientId;
        }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<ClientDto>>
    {
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;
        private readonly ICacheServices _cacheServices;
        public DeleteClientCommandHandler(IClientServices clientServices, IMapper mapper, ICacheServices cacheServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _cacheServices = cacheServices;
        }

        public async Task<Response<ClientDto>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientServices.GetClientById(request.ClientId);

            await _clientServices.DeleteClient(client);

            await _cacheServices.RemoveByPrefixAsync("clients_list", cancellationToken);

            var clientDto = _mapper.Map<ClientDto>(client);

            return new Response<ClientDto>(clientDto, "Client deleted successfully");
        }
    }
}