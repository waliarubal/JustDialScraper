using PuppeteerSharp;
using PuppeteerSharp.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{

    public class JustDialService : IJustDialService
    {
        const string JUST_DIAL_URL = "https://www.justdial.com/";

        public void Dispose()
        {
            
        }

        public async Task<IEnumerable<string>> GetLocations(string keyword)
        {
            var locations = new List<string>();

            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);

            var browserOptions = new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                Timeout = 30000
            };

            var browser = await Puppeteer.LaunchAsync(browserOptions);

            var page = await browser.NewPageAsync();
            await page.GoToAsync(JUST_DIAL_URL);

            var cityInput = await page.WaitForSelectorAsync("#city");
            if (cityInput == null)
                return locations;

            await cityInput.FocusAsync();
            await cityInput.TypeAsync(string.Empty);
            await page.Keyboard.PressAsync(Key.Enter);

            var citiesDropdown = await page.WaitForSelectorAsync("span[class=\"city-drop dn\"]");
            if (citiesDropdown == null)
                return locations;

            var cityNodes = await page.QuerySelectorAllAsync("span[class=\"city-drop dn\"]>ul>li");
            foreach(var cityNode in cityNodes)
            {
                var idProperty = await cityNode.GetPropertyAsync("id");
                var cityName = idProperty.RemoteObject.Value.ToString();

                locations.Add(cityName);
            }

            await page.DisposeAsync();

            await browser.DisposeAsync();

            //await Driver.GoToUrl(JUST_DIAL_URL);

            //var cityInputNode = await Driver.FindElementById("city", WaitTime);
            //if (cityInputNode == null)
            //    return locations;

            //await cityInputNode.Click();
            //await cityInputNode.Clear();
            //if (!string.IsNullOrWhiteSpace(keyword))
            //    await cityInputNode.SendKeys(keyword);   
            //await cityInputNode.SendKeys(Keys.Return);

            //var cityDropDownNode = await Driver.FindElementByCssSelector("span[class=\"city-drop dn\"]", WaitTime);
            //if (cityDropDownNode == null)
            //    return locations;

            //var cityNodes = await Driver.FindElementsByCssSelector("span[class=\"city-drop dn\"]>ul>li", WaitTime);
            //if (cityNodes == null)
            //    return locations;

            //foreach (var cityNode in cityNodes)
            //{
            //    var cityName = await cityNode.GetAttribute("id");
            //    locations.Add(cityName);
            //}

            return locations;
        }
    }
}
