using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Domain.Entities;

namespace simple_Web.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByEmailAsync(string email);
    }
}
