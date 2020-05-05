using JustDialScraper.Common.Base;

namespace JustDialScraper.Ui.Models
{
    public class SearchParameterModel: ModelBase
    {
        public int MaxListingsToTraverse
        {
            get => Get<int>();
            set => Set(value);
        }

        public int ConcurrentWebRequests
        {
            get => Get<int>();
            set => Set(value);
        }
    }
}
