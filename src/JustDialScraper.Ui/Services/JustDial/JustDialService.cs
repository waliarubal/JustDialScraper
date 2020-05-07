using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Zu.AsyncWebDriver.Remote;
using Zu.Chrome;

namespace JustDialScraper.Ui.Services
{

    public class JustDialService : IJustDialService
    {
        const string JUST_DIAL_URL = "https://www.justdial.com/";

        WebDriver _driver;

        WebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var isHeadless = !Debugger.IsAttached;

                    var config = new ChromeDriverConfig();
                    config.Headless = isHeadless;

                    var chromeDriver = new AsyncChromeDriver(config);

                    _driver = new WebDriver(chromeDriver);
                }

                return _driver;
            }
        }

        public void Dispose()
        {
            if (_driver == null)
                return;

            Driver.Close();
            Driver.Dispose();
        }

        public async Task<IEnumerable<string>> GetLocations(string keyword)
        {
            var locations = new List<string>();

            await Driver.Options().Timeouts.SetImplicitWait(TimeSpan.FromSeconds(10));

            await Driver.GoToUrl(JUST_DIAL_URL);

            var cityInputNode = await Driver.FindElementByCssSelector("input[id=\"city\"]");
            if (cityInputNode == null)
                return locations;


            if (string.IsNullOrWhiteSpace(keyword))
                await cityInputNode.Click();
            else
            {
                await cityInputNode.Click();
                await cityInputNode.Clear();
                await cityInputNode.SendKeys(keyword);
            }

            var cityDropDownNode = await Driver.FindElementByCssSelector("span[class=\"city-drop dn\"]");
            if (cityDropDownNode == null)
                return locations;

            var cityNodes = await Driver.FindElementsByCssSelector("span[class=\"city-drop dn\"]>ul>li");
            if (cityNodes == null)
                return locations;

            foreach (var cityNode in cityNodes)
            {
                var cityName = await cityNode.GetAttribute("id");
                locations.Add(cityName);
            }

            return locations;
        }
    }
}
