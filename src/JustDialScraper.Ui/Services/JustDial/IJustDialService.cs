using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public interface IJustDialService: IDisposable
    {
        Task<IEnumerable<string>> GetLocations(string keyword);
    }
}
