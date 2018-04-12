using System;

using CellTracker.Repository.Entities;

namespace CellTracker.Services.Dto
{
    public class SmsRecordDto
    {
        public string SubscriberId { get; set; }

        public DateTimeOffset Timestmap { get; set; }

        public static SmsRecordDto FromEntity(SmsRecord entity)
            => new SmsRecordDto
            {
                SubscriberId = entity.SubscriberId,
                Timestmap = entity.Timestamp
            };
    }
}