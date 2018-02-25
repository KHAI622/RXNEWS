using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace NewsFeed.NewsProvider
{
    internal sealed class MouseClickEventNewsProvider : INewsProvider
    {
        public MouseClickEventNewsProvider(UIElement uiElement)
        {
            News = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(
                handler => handler.Invoke,
                handler => uiElement.MouseLeftButtonDown += handler,
                handler => uiElement.MouseLeftButtonDown -= handler)
                .Select(p =>
                {
                    Point position = p.EventArgs.GetPosition((IInputElement)p.Sender);
                    return $"User click. X: {position.X} Y: {position.Y} (executing on Thread {Thread.CurrentThread.ManagedThreadId})";
                });
        }

        public IObservable<string> News { get; }
    }
}