using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using NewsFeed.Command;
using NewsFeed.Common;
using NewsFeed.NewsProvider;

namespace NewsFeed.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly IDialog _dialog;
        private readonly INewsProvider[] _newsProviders;
        private IObservable<string> _newsObservable;
        private ICommand _showNewsInAppCommand;
        private ICommand _showNewsOnConsoleCommand;
        private ICommand _showNewsInOutputCommand;
        private ICommand _stopNewsInAppCommand;
        private ICommand _stopNewsOnConsoleCommand;
        private ICommand _stopNewsInOutputCommand;
        private IDisposable _appSubscription;
        private IDisposable _consoleSubscription;
        private IDisposable _outputSubscription;

        public MainWindowViewModel(
            ISchedulerProvider schedulerProvider,
            IDialog dialog,
            params INewsProvider[] newsProviders)
        {
            _schedulerProvider = schedulerProvider;
            _dialog = dialog;
            _newsProviders = newsProviders;
            LoadNews();
        }

        public ObservableCollection<string> NewsFeed { get; } = new ObservableCollection<string>();

        public ICommand ShowNewsInAppCommand => _showNewsInAppCommand ?? (_showNewsInAppCommand = new ActionCommand(ShowNewsInApp));

        public ICommand ShowNewsOnConsoleCommand => _showNewsOnConsoleCommand ?? (_showNewsOnConsoleCommand = new ActionCommand(ShowNewsOnConsole));

        public ICommand ShowNewsInOutputCommand => _showNewsInOutputCommand ?? (_showNewsInOutputCommand = new ActionCommand(ShowNewsInOutput));

        public ICommand StopNewsInAppCommand => _stopNewsInAppCommand ?? (_stopNewsInAppCommand = new ActionCommand(StopNewsInApp));

        public ICommand StopNewsOnConsoleCommand => _stopNewsOnConsoleCommand ?? (_stopNewsOnConsoleCommand = new ActionCommand(StopNewsOnConsole));

        public ICommand StopNewsInOutputCommand => _stopNewsInOutputCommand ?? (_stopNewsInOutputCommand = new ActionCommand(StopNewsInOutput));

        private void LoadNews()
        {
            _newsObservable = _newsProviders
                .Select(p => p.News)
                .Merge()
                .SubscribeOn(_schedulerProvider.NewThreadScheduler)
                .ObserveOn(_schedulerProvider.DispatcherScheduler);
        }

        private void ShowNewsInApp()
        {
            _appSubscription = _newsObservable
                .Subscribe(news => NewsFeed.Add(news),
                ex => _dialog.Open(ex.Message, "Error"),
                () => _dialog.Open("News feed completed.", "Info"));
        }

        private void ShowNewsOnConsole()
        {
            _consoleSubscription = _newsObservable
                .Subscribe(news => Console.WriteLine(news),
                ex => Console.WriteLine($"Error: {ex.Message}"),
                () => Console.WriteLine("Info: News feed completed."));
        }

        private void ShowNewsInOutput()
        {
            _outputSubscription = _newsObservable
                .Subscribe(news => Debug.WriteLine(news),
                ex => Debug.WriteLine($"Error: {ex.Message}"),
                () => Debug.WriteLine("Info: News feed completed."));
        }

        private void StopNewsInApp()
        {
            _appSubscription.Dispose();
        }

        private void StopNewsOnConsole()
        {
            _consoleSubscription.Dispose();
        }

        private void StopNewsInOutput()
        {
            _outputSubscription.Dispose();
        }
    }
}