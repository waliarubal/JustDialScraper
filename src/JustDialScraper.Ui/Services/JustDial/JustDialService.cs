using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zu.AsyncWebDriver.Remote;
using Zu.Chrome;
using Zu.WebBrowser.BasicTypes;

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
                    var isHeadless = false; //!Debugger.IsAttached;

                    var config = new ChromeDriverConfig();
                    config.SetCommandLineArgumets("--disable-gpu --window-size=1280,800");
                    config.Headless = isHeadless;

                    var chromeDriver = new AsyncChromeDriver(config);

                    _driver = new WebDriver(chromeDriver);
                }

                return _driver;
            }
        }

        TimeSpan WaitTime { get; } = TimeSpan.FromSeconds(10);

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

            await Driver.GoToUrl(JUST_DIAL_URL);

            var cityInputNode = await Driver.FindElementById("city", WaitTime);
            if (cityInputNode == null)
                return locations;

            await cityInputNode.Click();
            await cityInputNode.Clear();
            if (!string.IsNullOrWhiteSpace(keyword))
                await cityInputNode.SendKeys(keyword);   
            await cityInputNode.SendKeys(Keys.Return);

            var cityDropDownNode = await Driver.FindElementByCssSelector("span[class=\"city-drop dn\"]", WaitTime);
            if (cityDropDownNode == null)
                return locations;

            var cityNodes = await Driver.FindElementsByCssSelector("span[class=\"city-drop dn\"]>ul>li", WaitTime);
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
