using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Application.Dtos.Auth;
using SmartInvoice.Application.Features.Auth.Commands;
using SmartInvoice.Application.Features.Auth.Commands.AuthCommands;

namespace SmartInvoice.API.Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpPost("signin")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await Mediator!.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GetIpAddress()
            }));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            return Ok(await Mediator!.Send(new RegisterCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["Origin"].FirstOrDefault() ?? string.Empty
            }));
        }
        
        private string GetIpAddress()
        {
            if (Request.Headers.TryGetValue("X-Forwarded-For", out var forwarderIp) && !string.IsNullOrWhiteSpace(forwarderIp))
            {
                return forwarderIp.ToString();
            }

            var remoteIp = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIp != null ? remoteIp.MapToIPv4().ToString() : "Ip no valida";
        }

    }
}