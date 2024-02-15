using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace simple_Web.DataAccess.Interfaces.Common
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public Task<int> SaveChangesAsync();
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
