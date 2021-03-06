﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public class PlatformService : IPlatformService
    {
        public Version GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public async Task<TResult> OpenModal<TView, TResult>() where TView : class
        {
            var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (lifetime == null)
                return default;

            var view = Activator.CreateInstance<TView>() as Window;
            if (view == null)
                return default;

            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = await view.ShowDialog<TResult>(lifetime.MainWindow);
            return result;
        }
    }
}
