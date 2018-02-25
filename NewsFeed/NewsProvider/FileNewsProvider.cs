using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading;

namespace NewsFeed.NewsProvider
{
    internal sealed class FileNewsProvider : INewsProvider
    {
        private const int NewsCount = 10;

        public FileNewsProvider()
        {
            PopulateNewsToFile();
        }

        public IObservable<string> News
        {
            get
            {
                return Observable.Create<string>(observer =>
                {
                    var file = new StreamReader(@"News.txt");
                    while (!file.EndOfStream)
                    {
                        observer.OnNext($"{file.ReadLine()} (executing on Thread {Thread.CurrentThread.ManagedThreadId}).");
                    }

                    observer.OnCompleted();

                    return (() =>
                    {
                        file.Dispose();
                    });
                });
            }
        }

        private static void PopulateNewsToFile()
        {
            using (var file = new StreamWriter(@"News.txt"))
            {
                for (int i = 0; i < NewsCount; i++)
                {
                    file.WriteLine($"News #{i + 1} from File");
                }
            }
        }
    }
}