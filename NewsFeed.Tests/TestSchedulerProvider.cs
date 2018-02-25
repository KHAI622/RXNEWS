using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using NewsFeed.Common;

namespace NewsFeed.Tests
{
    public class TestSchedulerProvider : ISchedulerProvider
    {
        public TestSchedulerProvider()
        {
            var testScheduler = new TestScheduler();
            NewThreadScheduler = testScheduler;
            DispatcherScheduler = testScheduler;
            TestScheduler = testScheduler;
        }

        public IScheduler NewThreadScheduler { get; }

        public IScheduler DispatcherScheduler { get; }

        public TestScheduler TestScheduler { get; }
    }
}