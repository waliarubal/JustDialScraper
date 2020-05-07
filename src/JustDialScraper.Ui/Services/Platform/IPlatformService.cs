using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public interface IPlatformService
    {
        Task<TResult> OpenModal<TView, TResult>() where TView: class;
    }
}
