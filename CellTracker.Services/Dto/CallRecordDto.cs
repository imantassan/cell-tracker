using System;
using System.Linq;

using CellTracker.Repository.Entities;

namespace CellTracker.Services.Dto
{
    public class CallRecordDto
    {
        public string SubscriberId { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public int TotalCount { get; set; }

        public static CallRecordDto FromEntityGroup(IGrouping<string, CallRecord> entity)
            => new CallRecordDto
            {
                TotalDuration = TimeSpan.FromSeconds(entity.Sum(x => x.Duration)),
                TotalCount = entity.Count(),
                SubscriberId = entity.Key
            };
    }
}