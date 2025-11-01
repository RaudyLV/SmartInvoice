using System.IdentityModel.Tokens.Jwt;
using SmartInvoice.Application.Dtos.Auth;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly JWTServices _jwtServices;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleManager;

        public AuthServices(JWTServices jwtServices, IPasswordHasher passwordHasher,
            IUserServices userServices, IUserRoleServices userRoleManager)
        {
            _jwtServices = jwtServices;
            _passwordHasher = passwordHasher;
            _userServices = userServices;
            _userRoleManager= userRoleManager;
        }

        public async Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userServices.FindUserByEmailAsync(request.Email);

            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new UnAuthorizedException("Invalid email or password");
            }

            var roles = user.UserRoles;
            var jwtToken = _jwtServices.GetJwt(user.Id, user.Username, user.Email, roles);
            
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var response = new AuthenticationResponse
            {
                Id = user.Id,
                UserName = user.Username,
                Email = user.Email,
                Roles = roles,
                Token = token,
                IsAuthenticated = true
            };

            return new Response<AuthenticationResponse>(response, message: "Login successful");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var emailExists = await _userServices.FindUserByEmailAsync(request.Email);
            if (emailExists != null)
            {
                throw new BadRequestException("Email already exists");
            } 

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
            };

            await _userServices.CreateUserAsync(user);

            await _userRoleManager.AddToRoleAsync(user, Roles.User.ToString());

            return new Response<string>(user.Id.ToString(), message: "User registered successfully");
        }
    }
}