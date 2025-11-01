using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Clients.Queries
{
    public class FindClientByQuery : IRequest<Response<ClientDto>>
    {
        public string Query { get; set; }

        public FindClientByQuery(string query)
        {
            Query = query;
        }
    }

    public class FindClientByQueryHandler : IRequestHandler<FindClientByQuery, Response<ClientDto>>
    {
        private readonly IClientServices _clientServices;

        public FindClientByQueryHandler(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        public async Task<Response<ClientDto>> Handle(FindClientByQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientServices.FindClientByQuery(request.Query);

            return new Response<ClientDto>(client);
        }
    }

}