using RestSharp;
using RestSharp.Serialization;
using System;

namespace JustDialScraper.Ui
{
    class RestSerializer : IRestSerializer
    {
        public string[] SupportedContentTypes { get; } = 
        {
            "application/json", 
            "text/json", 
            "text/x-json", 
            "text/javascript", 
            "*+json", 
            "text/html; charset=UTF-8"
        };

        public DataFormat DataFormat => DataFormat.Json;

        public string ContentType { get; set; } = "application/json";

        public T Deserialize<T>(IRestResponse response)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(response.RawBytes);
        }

        public string Serialize(Parameter bodyParameter) => Serialize(bodyParameter.Value);

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
