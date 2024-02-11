using simple_Web.Domain.Comman;
using System.Linq.Expressions;

namespace simple_Web.DataAccess.Interfaces.Common
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll();

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
