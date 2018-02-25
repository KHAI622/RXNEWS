using System.Windows;

namespace NewsFeed.Common
{
    public class MessageBoxDialog : IDialog
    {
        public void Open(string message, string title = null)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
    }
}