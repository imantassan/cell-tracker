using System;
using System.Collections.Generic;

namespace CellTracker.Services
{
    public interface ILogService<out TDto> where TDto : class
    {
        IReadOnlyCollection<TDto> GetTopForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo, int count);

        int GetTotalCountForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo);
    }
}