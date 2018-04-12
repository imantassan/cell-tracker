using CellTracker.Services;
using CellTracker.Services.Dto;

namespace CellTracker.Controllers
{
    public class CallLogController : LogController<CallRecordDto>
    {
        public CallLogController(ILogService<CallRecordDto> service)
            : base(service)
        {
        }
    }
}