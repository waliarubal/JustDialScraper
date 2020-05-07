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

            var lifetime = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (lifetime != null)
            {
                lifetime.MainWindow = new MainView();
                lifetime.Exit += Exit;
            }

            base.OnFrameworkInitializationCompleted();
        }

        void Exit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            var lifetime = sender as IClassicDesktopStyleApplicationLifetime;
            if (lifetime != null)
                lifetime.Exit -= Exit;

            ServiceLocator.Instance.Resolve<IJustDialService>().Dispose();
        }
    }
}
