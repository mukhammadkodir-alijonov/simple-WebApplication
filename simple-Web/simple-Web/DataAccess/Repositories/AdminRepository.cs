using Microsoft.EntityFrameworkCore;
using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces;
using simple_Web.DataAccess.Repositories.Comman;
using simple_Web.Domain.Entities;

namespace simple_Web.DataAccess.Repositories
{
    public class AdminRepository :GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext appDbContext) :  base(appDbContext)
        {
            
        }
        public async Task<Admin?> GetByEmailAsync(string email)
            => await _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == email);
    }
}
