using System;

using CellTracker.Model;
using CellTracker.Services;

using Microsoft.AspNetCore.Mvc;

namespace CellTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class LogController<TDto> : Controller
        where TDto : class
    {
        private readonly ILogService<TDto> service;

        protected LogController(ILogService<TDto> service)
        {
            this.service = service;
        }


        [HttpGet]
        public LogResponse<TDto> Get(DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            if (!dateFrom.HasValue || !dateTo.HasValue || dateFrom > dateTo)
            {
                return new LogResponse<TDto>
                {
                    Success = false
                };
            }

            return new LogResponse<TDto>
            {
                Success = true,
                TotalCount = service.GetTotalCountForPeriod(dateFrom.Value, dateTo.Value),
                TopRecords = service.GetTopForPeriod(dateFrom.Value, dateTo.Value, 5)
            };
        }
    }
}
