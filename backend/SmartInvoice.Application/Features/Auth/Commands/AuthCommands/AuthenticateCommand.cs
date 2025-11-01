using MediatR;
using SmartInvoice.Application.Dtos.Auth;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Auth.Commands.AuthCommands
{
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAuthServices _authService;

        public AuthenticateCommandHandler(IAuthServices authService)
        {
            _authService = authService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password,
            }, request.IpAddress);
            
        }
    }
}