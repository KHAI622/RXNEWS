using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsFeed.Common;
using NewsFeed.NewsProvider;
using NewsFeed.ViewModel;

namespace NewsFeed.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void ShowNewsInAppTest()
        {
            var testSchedulerProvider = new TestSchedulerProvider();
            var newsProviderMock = new Mock<INewsProvider>();
            var newsList = new List<string>
            {
                "News 1",
                "News 2",
                "News 3"
            };
            newsProviderMock.Setup(p => p.News).Returns(newsList.ToObservable());
            var dialog = new Mock<IDialog>();
            dialog.Setup(p => p.Open(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mainWindowViewModel = new MainWindowViewModel(testSchedulerProvider, dialog.Object, newsProviderMock.Object);
            mainWindowViewModel.ShowNewsInAppCommand.Execute(null);

            testSchedulerProvider.TestScheduler.AdvanceBy(5);

            CollectionAssert.AreEqual(newsList, mainWindowViewModel.NewsFeed);
            dialog.Verify(p => p.Open(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ShowNewsOnConsoleTest()
        {
            var testSchedulerProvider = new TestSchedulerProvider();
            var newsProviderMock = new Mock<INewsProvider>();
            var newsList = new List<string>
            {
                "News 1",
                "News 2",
                "News 3"
            };
            newsProviderMock.Setup(p => p.News).Returns(newsList.ToObservable());
            var dialog = new Mock<IDialog>();

            var mainWindowViewModel = new MainWindowViewModel(testSchedulerProvider, dialog.Object, newsProviderMock.Object);
            mainWindowViewModel.ShowNewsOnConsoleCommand.Execute(null);

            testSchedulerProvider.TestScheduler.AdvanceBy(5);

            CollectionAssert.AreEqual(newsList, mainWindowViewModel.NewsFeed);
        }

        [TestMethod]
        public void ShowNewsInOutputTest()
        {
            var testSchedulerProvider = new TestSchedulerProvider();
            var newsProviderMock = new Mock<INewsProvider>();
            var newsList = new List<string>
            {
                "News 1",
                "News 2",
                "News 3"
            };
            newsProviderMock.Setup(p => p.News).Returns(newsList.ToObservable());
            var dialog = new Mock<IDialog>();

            var mainWindowViewModel = new MainWindowViewModel(testSchedulerProvider, dialog.Object, newsProviderMock.Object);
            mainWindowViewModel.ShowNewsInOutputCommand.Execute(null);

            testSchedulerProvider.TestScheduler.AdvanceBy(5);

            CollectionAssert.AreEqual(newsList, mainWindowViewModel.NewsFeed);
        }
    }
}