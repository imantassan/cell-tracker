using System;
using System.Collections.Generic;
using System.Linq;

using CellTracker.Common.Helpers;
using CellTracker.Repository;
using CellTracker.Repository.Entities;

using Microsoft.EntityFrameworkCore;

namespace CellTracker.Helpers
{
    internal static class LogDbContextExtensions
    {
        public static void Seed(this LogDbContext context)
        {
            var subscribers = Enumerable.Range(0, 100).Select(_ => RandomDataGenerator.GetPhoneNumber()).ToList();

            SeedCallRecords(context.GetDbSet<CallRecord>(), subscribers);
            SeedShortMessages(context.GetDbSet<SmsRecord>(), subscribers);

            context.Save();
        }

        private static void SeedCallRecords(DbSet<CallRecord> dbSet, IList<string> subscribers)
        {
            if (!dbSet.Any())
            {
                for (var i = 0; i < 10000; i++)
                {
                    dbSet.Add(
                        new CallRecord
                        {
                            Duration = RandomDataGenerator.GetNumber(0, 3600),
                            SubscriberId = subscribers[RandomDataGenerator.GetNumber(0, subscribers.Count)],
                            Timestamp = RandomDataGenerator.GetDate()
                        });
                }
            }
        }

        private static void SeedShortMessages(DbSet<SmsRecord> dbSet, IList<string> subscribers)
        {
            if (!dbSet.Any())
            {
                for (var i = 0; i < 10000; i++)
                {
                    dbSet.Add(
                        new SmsRecord
                        {
                            SubscriberId = subscribers[RandomDataGenerator.GetNumber(0, subscribers.Count)],
                            Timestamp = RandomDataGenerator.GetDate()
                        });
                }
            }
        }
    }
}