using System.Runtime.InteropServices;
using System.Windows;
using NewsFeed.Common;
using NewsFeed.NewsProvider;
using NewsFeed.ViewModel;

namespace NewsFeed
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var mainWindow = new MainWindow();

            var enumerableNewsProvider = new EnumerableNewsProvider();
            var fileNewsProvider = new FileNewsProvider();
            var mouseClickEventNewsProvider = new MouseClickEventNewsProvider(mainWindow.ClickMeBorder);
            var newsFeedServiceClient = new NewsFeedService.NewsFeedServiceClient();
            var timerNewsProvider = new TimerNewsProvider(newsFeedServiceClient);
            var taskNewsProvider = new TaskNewsProvider();
            var schedulerProvider = new SchedulerProvider();
            var messageBoxDialog = new MessageBoxDialog();
            var mainWindowViewModel = new MainWindowViewModel(
                schedulerProvider,
                messageBoxDialog,
                enumerableNewsProvider,
                fileNewsProvider,
                mouseClickEventNewsProvider,
                timerNewsProvider,
                taskNewsProvider);

            mainWindow.DataContext = mainWindowViewModel;

            AllocConsole();
            mainWindow.Show();
        }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();
    }
}