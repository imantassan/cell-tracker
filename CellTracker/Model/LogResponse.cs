using System.Collections.Generic;

namespace CellTracker.Model
{
    public class LogResponse<TDto>
    {
        public bool Success { get; set; }

        public int TotalCount { get; set; }

        public IReadOnlyCollection<TDto> TopRecords { get; set; }
    }
}