using Avalonia.Controls;

namespace JustDialScraper.Common.Base
{
    public abstract class ViewModelBase : ModelBase
    {
        protected ViewModelBase()
        {

        }

        #region properties

        public abstract bool IsCachable { get; }

        protected bool IsDesignMode => Design.IsDesignMode;

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

        protected T Resolve<T>() where T : class
        {
            if (IsDesignMode)
                return default;

            return ServiceLocator.Instance.Resolve<T>();
        }
    }
}
