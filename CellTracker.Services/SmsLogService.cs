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

        protected override IOrderedQueryable<SmsRecord> ApplyOrdering(IQueryable<SmsRecord> query) => query.OrderByDescending(x => x.Timestamp);

        protected override SmsRecordDto ConvertToDto(SmsRecord entity) => SmsRecordDto.FromEntity(entity);
    }
}