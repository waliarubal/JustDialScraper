using JustDialScraper.Common.Base;
using JustDialScraper.Common.Commands;
using JustDialScraper.Ui.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JustDialScraper.Ui.ViewModels
{
    public class ManageViewModel : ViewModelBase
    {
        readonly IJustDialService _justDialService;
        ICommand _search;

        public ManageViewModel(IJustDialService justDialService)
        {
            _justDialService = justDialService;

            SearchResults = new ObservableCollection<string>();
        }

        #region properties

        public override bool IsCachable => false;

        public string Keyword
        {
            get => Get<string>();
            set => Set(value);
        }

        public ObservableCollection<string> SearchResults
        {
            get => Get<ObservableCollection<string>>();
            private set => Set(value);
        }

        #endregion

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
            var results = await _justDialService.GetLocations(keyword);
            SearchResults = new ObservableCollection<string>(results);
        }
    }
}
