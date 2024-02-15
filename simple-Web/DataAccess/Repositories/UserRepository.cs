using Microsoft.EntityFrameworkCore;
using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces;
using simple_Web.DataAccess.Repositories.Comman;
using simple_Web.Domain.Entities;

namespace simple_Web.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
            => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
