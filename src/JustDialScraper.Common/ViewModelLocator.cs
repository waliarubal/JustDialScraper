using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;
using JustDialScraper.Common.Base;
using System;
using System.Globalization;
using System.Reflection;

[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "JustDialScraper.Common")]

namespace JustDialScraper.Common
{
    public static class ViewModelLocator
    {
        public static readonly AvaloniaProperty AutoWireViewModelProperty;

        static ViewModelLocator()
        {
            AutoWireViewModelProperty = AvaloniaProperty.RegisterAttached<Control, bool>("", typeof(ViewModelLocator), false);
            AutoWireViewModelProperty.Changed.Subscribe(args => AutoWireViewModelChanged(args?.Sender, args));
        }

        public static bool GetAutoWireViewModel(AvaloniaObject control)
        {
            return (bool)control.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(AvaloniaObject control, bool value)
        {
            control.SetValue(AutoWireViewModelProperty, value);
        }

        static void AutoWireViewModelChanged(AvaloniaObject control, AvaloniaPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
                return;
            
            var view = control as Control;
            if (view == null)
                return;

            var viewType = control.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
            var viewModelType = Type.GetType(viewModelName);

            var viewModel = ServiceLocator.Instance.Container.Resolve(viewModelType) as ViewModelBase;
            view.DataContext = viewModel;
            viewModel.IsLoaded = true;
        }
    }
}
