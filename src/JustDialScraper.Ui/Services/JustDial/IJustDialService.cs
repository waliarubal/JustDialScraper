using JustDialScraper.Ui.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public interface IJustDialService
    {
        Task<IEnumerable<LocationModel>> GetLocations(string keyword);
    }
}
