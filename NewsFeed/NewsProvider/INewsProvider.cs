using System;

namespace NewsFeed.NewsProvider
{
    public interface INewsProvider
    {
        IObservable<string> News { get; }
    }
}