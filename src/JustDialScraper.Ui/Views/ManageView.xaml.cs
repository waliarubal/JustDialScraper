using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustDialScraper.Ui.Views
{
    public class ManageView : Window
    {
        public ManageView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
