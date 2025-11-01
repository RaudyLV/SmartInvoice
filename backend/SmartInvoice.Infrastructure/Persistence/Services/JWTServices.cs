using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartInvoice.Application.Dtos.Auth;
using SmartInvoice.Infrastructure.Persistence.Helpers;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class JWTServices
    {
        private readonly JWTSettings _jwtSettings;
        public JWTServices(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtSecurityToken GetJwt(int userId, string username, string email, List<string> roles = null!)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string ip = IpHelper.GetIp();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("ip", ip),
                new Claim("uid", userId.ToString())
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_jwtSettings.DurationInMinutes)
                ),
                signingCredentials: credentials
            );

            return token;
        }


        
        public RefreshToken RefreshToken(string ip)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpireTime = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ip
            };
        }

        private string RandomTokenString()
        {
            var randomBytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return BitConverter.ToString(randomBytes)
                 .Replace("+", "") // Elimina '+' para mas legibilidad
                .Replace("/", "_") // reemplaza '/' y '+' para la URL
                .TrimEnd('='); //
        }
    }
}