using System;

using CellTracker.Repository.Entities;

namespace CellTracker.Services.Dto
{
    public class CallRecordDto
    {
        public string SubscriberId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public TimeSpan Duration { get; set; }

        public static CallRecordDto FromEntity(CallRecord entity)
            => new CallRecordDto
            {
                Timestamp = entity.Timestamp,
                Duration = entity.Duration,
                SubscriberId = entity.SubscriberId
            };
    }
}