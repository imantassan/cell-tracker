using CellTracker.Services;
using CellTracker.Services.Dto;

namespace CellTracker.Controllers
{
    public class SmsLogController : LogController<SmsRecordDto>
    {
        public SmsLogController(ILogService<SmsRecordDto> service)
            : base(service)
        {
        }
    }
}
