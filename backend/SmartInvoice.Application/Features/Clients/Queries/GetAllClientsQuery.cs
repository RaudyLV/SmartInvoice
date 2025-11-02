using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;


namespace SmartInvoice.Application.Features.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<Response<PagedList<ClientDto>>>
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set;} = "Name";
        public bool SortDescending  { get; set;} = true;
    }

    public class GetAllClientsQueryHandler : 
    IRequestHandler<GetAllClientsQuery, Response<PagedList<ClientDto>>>
    {
        private readonly IClientServices _clientServices;

        public GetAllClientsQueryHandler(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        public async Task<Response<PagedList<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clientsDto = await _clientServices.GetAllClientsAsync(request);

            int totalCount = await _clientServices.CountAsync(request.SearchTerm);

            var pagedList = PagedList<ClientDto>.Create(
                clientsDto,
                totalCount,
                request.PageNumber,
                request.PageSize);


            return new Response<PagedList<ClientDto>>(pagedList);
        }
    }

}