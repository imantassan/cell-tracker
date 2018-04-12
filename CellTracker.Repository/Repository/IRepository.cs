using System;
using System.Linq;
using System.Linq.Expressions;

namespace CellTracker.Repository.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        TEntity Find(object id);
    }
}