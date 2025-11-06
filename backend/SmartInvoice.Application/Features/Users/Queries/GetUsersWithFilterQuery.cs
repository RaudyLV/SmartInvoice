
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Queries
{
    public class GetUsersWithFilterQuery : IRequest<Response<PagedList<UserDto>>>
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; } 
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