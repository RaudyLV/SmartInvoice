
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Response<List<UserDto>>>
    {

    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
    {
        private readonly IUserServices _userServices;

        public GetAllUsersQueryHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userServices.GetAllUsersAsync();

            return new Response<List<UserDto>>(users);
        }
    }
}