using Microsoft.EntityFrameworkCore.ChangeTracking;
using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces;
using simple_Web.DataAccess.Interfaces.Common;

namespace simple_Web.DataAccess.Repositories.Comman
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _dbContext;
        public IUserRepository Users { get; }
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Users = new UserRepository(_dbContext);
        }
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
            => _dbContext.Entry(entity);

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
