using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JustDialScraper.Common;
using JustDialScraper.Ui.Services;
using JustDialScraper.Ui.Views;

namespace JustDialScraper.Ui
{
    public class App : Application
    {
        void BootstrapServices()
        {
            ServiceLocator.Instance.RegisterSingleton<IPlatformService, PlatformService>();
            ServiceLocator.Instance.RegisterSingleton<IJustDialService, JustDialService>();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            BootstrapServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainView();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
