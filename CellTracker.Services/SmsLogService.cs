using System.Linq;

using CellTracker.Repository.Entities;
using CellTracker.Repository.Repository;
using CellTracker.Services.Dto;

namespace CellTracker.Services
{
    public class SmsLogService : LogService<SmsRecord, SmsRecordDto>

    {
        public SmsLogService(IRepository<SmsRecord> repository)
            : base(repository)
        {
        }

        protected override IOrderedQueryable<IGrouping<string, SmsRecord>> ApplyGroupingAndOrdering(IQueryable<SmsRecord> query)
            => query
                .GroupBy(x => x.SubscriberId)
                .OrderByDescending(x => x.Count());

        protected override SmsRecordDto ConvertToDto(IGrouping<string, SmsRecord> entity) => SmsRecordDto.FromEntityGroup(entity);
    }
}