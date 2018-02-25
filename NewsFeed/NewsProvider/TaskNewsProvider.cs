using System;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace NewsFeed.NewsProvider
{
    public sealed class TaskNewsProvider : INewsProvider
    {
        public IObservable<string> News => Task<string>.Factory.StartNew(() => $"News from Task (executing on Thread {Thread.CurrentThread.ManagedThreadId}).").ToObservable();
    }
}