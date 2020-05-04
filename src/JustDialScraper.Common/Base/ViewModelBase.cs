namespace JustDialScraper.Common.Base
{
    public abstract class ViewModelBase : ModelBase
    {
        protected ViewModelBase()
        {

        }

        #region properties

        public abstract bool IsCachable { get; }

        public string Title
        {
            get => Get<string>();
            protected set => Set(value);
        }

        public bool IsBusy
        {
            get => Get<bool>();
            protected set => Set(value);
        }

        public bool IsLoaded
        {
            get => Get<bool>();
            internal set => Set(value);
        }

        #endregion
    }
}
