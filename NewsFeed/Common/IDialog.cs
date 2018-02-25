namespace NewsFeed.Common
{
    public interface IDialog
    {
        void Open(string message, string title = null);
    }
}