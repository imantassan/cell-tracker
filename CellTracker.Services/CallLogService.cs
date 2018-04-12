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

        protected override IOrderedQueryable<CallRecord> ApplyOrdering(IQueryable<CallRecord> query) => query.OrderByDescending(x => x.Duration);

        protected override CallRecordDto ConvertToDto(CallRecord entity) => CallRecordDto.FromEntity(entity);
    }
}