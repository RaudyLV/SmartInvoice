
using System.Security.Cryptography;
using System.Text;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Queries
{
    public class GetUsersWithFilterQuery : IRequest<Response<PagedList<UserDto>>>,
    ICacheableQuery
    {
        public string SearchTerm { get; set; } = null!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = null!;
        public bool SortDescending { get; set; }

        public string CacheKey
        {
            get
            {
                var keyComponents = $"{SearchTerm}" +
                                    $"{PageNumber}|{PageSize}|{SortBy}|{SortDescending}";

                var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyComponents));
                var hash = Convert.ToBase64String(hashBytes);

                return $"users_list_{hash}";
            }
        }

        public TimeSpan? CacheDuration => throw new NotImplementedException();
    }

    public class GetUsersWithFilterQueryHandler
    : IRequestHandler<GetUsersWithFilterQuery, Response<PagedList<UserDto>>>
    {
        private readonly IUserServices _userServices;
        public GetUsersWithFilterQueryHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Response<PagedList<UserDto>>> Handle(GetUsersWithFilterQuery request, CancellationToken cancellationToken)
        {
            var users = await _userServices.UsersWithFilterAsync(request);

            int totalCount = await _userServices.CountAsync(request.SearchTerm);

            var pagedList = PagedList<UserDto>.Create(
                users,
                totalCount,
                request.PageNumber,
                request.PageSize);

            return new Response<PagedList<UserDto>>(pagedList);
        }
    }
}