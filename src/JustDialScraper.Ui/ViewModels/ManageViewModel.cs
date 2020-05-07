using JustDialScraper.Common.Base;
using JustDialScraper.Common.Commands;
using JustDialScraper.Ui.Models;
using JustDialScraper.Ui.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JustDialScraper.Ui.ViewModels
{
    public class ManageViewModel : ViewModelBase
    {
        readonly IJustDialService _justDialService;
        ICommand _search;

        public ManageViewModel()
        {
            _justDialService = Resolve<IJustDialService>();
        }

        public override bool IsCachable => false;

        public string Keyword
        {
            get => Get<string>();
            set => Set(value);
        }

        public ObservableCollection<IJustDialRecord> SearchResults
        {
            get => Get<ObservableCollection<IJustDialRecord>>();
            private set => Set(value);
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_search == null)
                    _search = new RelayCommand<string>(SearchAction);

                return _search;
            }
        }

        async void SearchAction(string keyword)
        {
            var records = await _justDialService.GetLocations(keyword);
            SearchResults = new ObservableCollection<IJustDialRecord>(records);
        }
    }
}
