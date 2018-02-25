using System.Reactive.Concurrency;

namespace NewsFeed.Common
{
    public interface ISchedulerProvider
    {
        IScheduler NewThreadScheduler { get; }

        IScheduler DispatcherScheduler { get; }
    }
}