using simple_Web.Domain.Entities;
using System.Security.Cryptography;

namespace simple_Web.Service.Interfaces.Common
{
    public interface IAuthService
    {
        public string GenerateToken(Human human, string role);

    }
}
