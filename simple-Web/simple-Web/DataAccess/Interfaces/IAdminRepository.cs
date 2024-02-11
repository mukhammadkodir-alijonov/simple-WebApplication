using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Domain.Entities;

namespace simple_Web.DataAccess.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        public Task<Admin?> GetByEmailAsync(string email);
    }
}
