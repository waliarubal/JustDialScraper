using JustDialScraper.Common.Base;
using JustDialScraper.Common.Commands;
using JustDialScraper.Ui.Models;
using System;
using System.Reflection;
using System.Windows.Input;

namespace JustDialScraper.Ui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        ICommand _clear, _search;

        public MainViewModel()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version;
            Listings = new ListingCollectionModel();

            ClearCommand.Execute(null);
        }

        #region properties

        public override bool IsCachable => false;

        public SearchParameterModel SearchParameter
        {
            get => Get<SearchParameterModel>();
            private set => Set(value);
        }

        public ListingCollectionModel Listings { get; }

        public Version Version { get; }

        #endregion

        public ICommand ClearCommand
        {
            get
            {
                if (_clear == null)
                    _clear = new RelayCommand(ClearAction);

                return _clear;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_search == null)
                    _search = new RelayCommand<SearchParameterModel>(SearchAction);

                return _search;
            }
        }

        void SearchAction(SearchParameterModel searchParameter)
        {

        }

        void ClearAction()
        {
            SearchParameter = new SearchParameterModel
            {
                MaxListingsToTraverse = 500,
                ConcurrentWebRequests = 10
            };

            Listings.Clear();
        }
    }
}
