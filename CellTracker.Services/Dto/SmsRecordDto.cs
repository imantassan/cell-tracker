using System;
using System.Linq;

using CellTracker.Repository.Entities;

namespace CellTracker.Services.Dto
{
    public class SmsRecordDto
    {
        public string SubscriberId { get; set; }

        public int TotalCount { get; set; }

        public static SmsRecordDto FromEntityGroup(IGrouping<string, SmsRecord> entity)
            => new SmsRecordDto
            {
                SubscriberId = entity.Key,
                TotalCount = entity.Count()
            };
    }
}