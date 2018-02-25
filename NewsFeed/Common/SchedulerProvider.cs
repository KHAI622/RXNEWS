using System.Reactive.Concurrency;

namespace NewsFeed.Common
{
    public sealed class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler NewThreadScheduler => System.Reactive.Concurrency.NewThreadScheduler.Default;

        public IScheduler DispatcherScheduler => System.Reactive.Concurrency.DispatcherScheduler.Current;
    }
}