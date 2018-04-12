using System;
using System.Collections.Generic;
using System.Linq;

using CellTracker.Repository.Entities;
using CellTracker.Repository.Repository;

namespace CellTracker.Services
{
    public abstract class LogService<TEntity, TDto> : ILogService<TDto>
        where TEntity : LogRecord, new()
        where TDto : class
    {
        private readonly IRepository<TEntity> repository;

        protected LogService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        protected abstract IOrderedQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query);

        protected abstract TDto ConvertToDto(TEntity entity);

        public IReadOnlyCollection<TDto> GetTopForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo, int count)
        {
            return ApplyOrdering(
                    repository
                        .Find(x => x.Timestamp >= dateFrom && x.Timestamp <= dateTo))
                .Take(count)
                .Select(ConvertToDto)
                .ToList();
        }

        public int GetTotalCountForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            return repository
                .Find(x => x.Timestamp >= dateFrom && x.Timestamp <= dateTo)
                .Count();
        }
    }
}