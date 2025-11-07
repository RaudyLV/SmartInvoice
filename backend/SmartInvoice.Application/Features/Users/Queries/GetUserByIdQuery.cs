
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Response<UserDto>>, ICacheableQuery
    {
        public int Id { get; set; }

        public string CacheKey => $"product_{Id}";

        public TimeSpan? CacheDuration => TimeSpan.FromMinutes(10);
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private readonly IUserServices _userServices;

        public GetUserByIdQueryHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userServices.GetUserByIdAsync(request.Id);

            return new Response<UserDto>(user);
        }
    }
}