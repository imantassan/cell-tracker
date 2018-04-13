using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using CellTracker.Common.Helpers;
using CellTracker.Repository.Entities;
using CellTracker.Repository.Repository;
using CellTracker.Services;

using FluentAssertions;

using NSubstitute;

using NUnit.Framework;

namespace CellTracker.Tests.Services
{
    [TestFixture]
    public class CallLogServiceTest
    {
        private CallLogService service;
        private IRepository<CallRecord> repository;

        [SetUp]
        public void SetUp()
        {
            repository = Substitute.For<IRepository<CallRecord>>();
            service = new CallLogService(repository);
        }

        [Test]
        public void CallLogService_GetTop_ReturnsCorrectNumberOfResults()
        {
            // arrange
            var subscriber = "123456789";
            var fakeSet = new List<CallRecord>
            {
                CreateCallRecord(subscriber),
                CreateCallRecord(subscriber),
                CreateCallRecord(subscriber)
            };
            repository.Find(Arg.Any<Expression<Func<CallRecord, bool>>>()).Returns(c => fakeSet.AsQueryable().Where(c.Arg<Expression<Func<CallRecord, bool>>>()));

            // act
            var result = service.GetTopForPeriod(DateTime.Today, DateTime.Today.AddDays(1), 10);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
        }

        [Test]
        public void CallLogService_GetLessThanAvailable_ReturnsTheNumberOfResultsRequested()
        {
            // arrange
            const string subscriber = "123456789";
            var fakeSet = new List<CallRecord>
            {
                CreateCallRecord(subscriber),
                CreateCallRecord(subscriber),
                CreateCallRecord(subscriber),
                CreateCallRecord("111111111111")
            };
            repository.Find(Arg.Any<Expression<Func<CallRecord, bool>>>()).Returns(c => fakeSet.AsQueryable().Where(c.Arg<Expression<Func<CallRecord, bool>>>()));

            // act
            var result = service.GetTopForPeriod(DateTime.Today, DateTime.Today.AddDays(1), 1);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.Should().OnlyContain(x => x.SubscriberId == subscriber);
        }

        [Test]
        public void CallLogService_GetMoreThanAvailable_ReturnsTheNumberOfResultsRequested()
        {
            // arrange
            string[] subscribers = { "123456789000", "111111111111" };
            var fakeSet = new List<CallRecord>
            {
                CreateCallRecord(subscribers[0]),
                CreateCallRecord(subscribers[1])
            };
            repository.Find(Arg.Any<Expression<Func<CallRecord, bool>>>()).Returns(c => fakeSet.AsQueryable().Where(c.Arg<Expression<Func<CallRecord, bool>>>()));

            // act
            var result = service.GetTopForPeriod(DateTime.Today, DateTime.Today.AddDays(1), 1);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.Should().OnlyContain(x => subscribers.Contains(x.SubscriberId));
        }

        [Test]
        public void CallLogService_GetNarrowDateRange_ReturnsOnlResultsFromSpecifiedDates()
        {
            // arrange
            string[] subscribers = { "111111111111", "22222222222", "3333333333333" };
            var fakeSet = new List<CallRecord>
            {
                // past date
                CreateCallRecord(subscribers[0], DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1)),
                // matching date
                CreateCallRecord(subscribers[1]),
                // future date
                CreateCallRecord(subscribers[2], DateTime.Today.AddDays(1), DateTime.Today.AddDays(2))
            };
            repository.Find(Arg.Any<Expression<Func<CallRecord, bool>>>()).Returns(c => fakeSet.AsQueryable().Where(c.Arg<Expression<Func<CallRecord, bool>>>()));

            // act
            var result = service.GetTopForPeriod(DateTime.Today.AddSeconds(1), DateTime.Today.AddDays(1).AddSeconds(-1), 10);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.Should().OnlyContain(x => x.SubscriberId == subscribers[1]);
        }

        private static CallRecord CreateCallRecord(string subscriberId, DateTime? minDate = null, DateTime? maxDate = null)
        {
            minDate = minDate ?? DateTime.Today;
            maxDate = maxDate ?? DateTime.Today.AddDays(1);

            return new CallRecord
            {
                Duration = RandomDataGenerator.GetNumber(0, 3600),
                SubscriberId = subscriberId,
                Timestamp = RandomDataGenerator.GetDate(minDate, maxDate)
            };
        }
    }
}