using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;


namespace SmartInvoice.Application.Features.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<Response<PagedList<ClientDto>>>, ICacheableQuery
    {
        public string SearchTerm { get; set; } = null!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set;} = "Name";
        public bool SortDescending  { get; set;} = true;

        public string CacheKey
        {
            get
            {
                var keyComponents = $"{SearchTerm}" 
                                    + $"{PageNumber}|{PageSize}|{SortBy}|{SortDescending}";

                var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyComponents));
                var hash = Convert.ToBase64String(hashBytes);
                                
                return $"clients_list_{hash}";
            }
        }

        public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
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