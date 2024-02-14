using Microsoft.IdentityModel.Tokens;
using simple_Web.Domain.Entities;
using simple_Web.Service.Interfaces.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace simple_Web.Service.Services.Common
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            this._config = config.GetSection("Jwt");
        }
        public string GenerateToken(Human user, string role)
        {
            var claims = new[]
               {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Lifetime"]!)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

}
