using System;
using System.Reactive.Linq;
using System.Threading;
using System.Timers;
using NewsFeed.NewsFeedService;
using Timer = System.Timers.Timer;

namespace NewsFeed.NewsProvider
{
    internal sealed class TimerNewsProvider : INewsProvider
    {
        private readonly INewsFeedService _newsFeedService;

        public TimerNewsProvider(INewsFeedService newsFeedService)
        {
            _newsFeedService = newsFeedService;

            News = Observable.Create<string>(observer =>
            {
                var timer = new Timer
                {
                    Enabled = true,
                    Interval = TimeSpan.FromSeconds(2).TotalMilliseconds
                };

                var timerOnElapsed = new ElapsedEventHandler((sender, args) =>
                {
                    observer.OnNext($"{_newsFeedService.GetNews()} (executing on Thread {Thread.CurrentThread.ManagedThreadId}).");
                });

                timer.Elapsed += timerOnElapsed;
                timer.Start();

                return () =>
                {
                    timer.Elapsed -= timerOnElapsed;
                    timer.Dispose();
                };
            });
        }

        public IObservable<string> News { get; }
    }
}