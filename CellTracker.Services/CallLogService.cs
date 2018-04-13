using System.Linq;

using CellTracker.Repository.Entities;
using CellTracker.Repository.Repository;
using CellTracker.Services.Dto;

namespace CellTracker.Services
{
    public class CallLogService : LogService<CallRecord, CallRecordDto>
    {
        public CallLogService(IRepository<CallRecord> repository)
            : base(repository)
        {
        }

        protected override IOrderedQueryable<IGrouping<string, CallRecord>> ApplyGroupingAndOrdering(IQueryable<CallRecord> query) => query.GroupBy(x => x.SubscriberId)
            .OrderByDescending(x => x.Sum(c => c.Duration));

        protected override CallRecordDto ConvertToDto(IGrouping<string, CallRecord> entity) => CallRecordDto.FromEntityGroup(entity);
    }
}