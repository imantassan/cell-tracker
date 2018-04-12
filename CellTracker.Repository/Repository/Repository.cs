using System;
using System.Linq;
using System.Linq.Expressions;

namespace CellTracker.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        protected IDbContext Context { get; }

        public Repository(IDbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return Context.GetDbSet<TEntity>().Where(filter);
        }

        public TEntity Find(object id)
        {
            return Context.GetDbSet<TEntity>().Find(id);
        }
    }
}