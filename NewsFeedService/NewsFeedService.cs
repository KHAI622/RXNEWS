using System;
using System.Threading.Tasks;

namespace NewsFeedService
{
    public class NewsFeedService : INewsFeedService
    {
        public Task<string> GetNewsAsync()
        {
            return Task<string>.Factory.StartNew(() => $"News #{new Random().Next(int.MaxValue)} from Service");
        }
    }
}