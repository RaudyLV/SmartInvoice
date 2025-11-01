
using SmartInvoice.Application.Dtos.Auth;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Interfaces
{
    public interface IAuthServices
    {
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request, string ipAddress);
    }
}