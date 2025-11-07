
using AutoMapper;
using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly ICacheServices _cacheServices;
        public DeleteUserCommandHandler(IMapper mapper, IUserServices userServices, ICacheServices cacheServices)
        {
            _mapper = mapper;
            _userServices = userServices;
            _cacheServices = cacheServices;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            await _userServices.DeleteUserAsync(user);

            await _cacheServices.RemoveByPrefixAsync("users_list", cancellationToken);

            return new Response<string>("User deleted successfully");
        }
    }
}