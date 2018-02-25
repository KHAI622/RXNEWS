using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;

namespace NewsFeed.NewsProvider
{
    internal sealed class EnumerableNewsProvider : INewsProvider
    {
        private const int NewsCount = 20;
        private readonly List<string> _newsList;

        public EnumerableNewsProvider()
        {
            _newsList = GetNewsList();
        }

        public IObservable<string> News => _newsList.ToObservable();

        private static List<string> GetNewsList()
        {
            var list = new List<string>();
            for (int i = 0; i < NewsCount; i++)
            {
                list.Add($"News #{i + 1} from Enumerable (executing on Thread {Thread.CurrentThread.ManagedThreadId}).");
            }

            return list;
        }
    }
}