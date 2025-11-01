using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;


namespace SmartInvoice.Application.Features.Clients.Queries
{
    public record GetAllClientsQuery : IRequest<Response<List<ClientDto>>>;

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Response<List<ClientDto>>>
    {
        private readonly IClientServices _clientServices;

        public GetAllClientsQueryHandler(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        public async Task<Response<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientServices.GetAllClientsAsync();

            return new Response<List<ClientDto>>(clients);
        }
    }

}