using JustDialScraper.Ui.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace JustDialScraper.Ui.Services
{
    public class GetLocationResponse
    {
        [DataMember(Name = "results")]
        public List<LocationModel> Locations { get; set; }
    }

    public class JustDialService : IJustDialService
    {
        const string JUST_DIAL_URL = "https://www.justdial.com/";

        RestClient _client;
        CookieContainer _cookieContainer;

        RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    _cookieContainer = new CookieContainer();

                    _client = new RestClient($"{JUST_DIAL_URL}webmain");
                    _client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0";
                    _client.AddDefaultHeader("Referer", JUST_DIAL_URL);
                    _client.CookieContainer = _cookieContainer;
                }

                return _client;
            }
        }

        public async Task<IEnumerable<LocationModel>> GetLocations(string keyword)
        {
            var request = new RestRequest("autosuggest.php");
            request.RequestFormat = DataFormat.Json;
            request.AddQueryParameter("cases", "popCity");
            request.AddQueryParameter("scity", "Delhi");
            request.AddQueryParameter("s", "1");
            request.AddQueryParameter("source", "10");
            request.AddQueryParameter("userid", "");
            if (!string.IsNullOrWhiteSpace(keyword))
                request.AddQueryParameter("search", keyword);

            var response = await Client.ExecuteGetAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return default;

            var x = response.ResponseUri.Segments;

            var data = Utf8Json.JsonSerializer.Deserialize<GetLocationResponse>(response.Content);
            return data.Locations;
        }
    }
}
