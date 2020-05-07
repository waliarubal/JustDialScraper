using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public interface IPlatformService
    {
        Task<TResult> OpenModal<TView, TResult>(double width, double height) where TView: class;
    }
}
