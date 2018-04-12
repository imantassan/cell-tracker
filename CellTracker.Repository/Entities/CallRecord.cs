using System;

namespace CellTracker.Repository.Entities
{
    public class CallRecord : LogRecord
    {
        public TimeSpan Duration { get; set; }
    }
}