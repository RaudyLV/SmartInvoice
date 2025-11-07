using MediatR;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAuthServices _authService;
        private readonly ICacheServices _cacheServices;
        public RegisterCommandHandler(IAuthServices authService, ICacheServices cacheServices)
        {
            _authService = authService;
            _cacheServices = cacheServices;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            await _cacheServices.RemoveByPrefixAsync("users_list", cancellationToken);

            return await _authService.RegisterAsync(new RegisterRequest
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
            }, request.Origin);
        }
    }
}