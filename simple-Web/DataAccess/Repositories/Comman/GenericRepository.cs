using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Domain.Comman;
using System.Linq.Expressions;

namespace simple_Web.DataAccess.Repositories.Comman
{
    public class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T>
        where T : BaseEntity
    {
        public GenericRepository(AppDbContext context) : base(context)
        {
        }
        public IQueryable<T> GetAll()
            => _dbSet;
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
            => _dbSet.Where(predicate);
    }
}
