using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public class PlatformService : IPlatformService
    {
        public async Task<TResult> OpenModal<TView, TResult>(double width, double height) where TView : class
        {
            var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (lifetime == null)
                return default;

            var view = Activator.CreateInstance<TView>() as Window;
            if (view == null)
                return default;

            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowInTaskbar = false;
            view.Width = width;
            view.Height = height;

            var result = await view.ShowDialog<TResult>(lifetime.MainWindow);
            return result;
        }
    }
}
