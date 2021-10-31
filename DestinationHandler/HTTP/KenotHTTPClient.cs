using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DestinationHandler.HTTP
{
    internal class KenotHTTPClient : IHTTPClient
    {
        private readonly HttpClient client = new();

        public async Task<TResponse> GetAsync<TRequest, TResponse>(string requestUri, TRequest request)
            where TRequest : IHTTPRequest
            where TResponse : IHTTPResponse
        {
            string param = GetRequestString(request);
            string fullRequestUri = $"{requestUri}?{param}";
            HttpResponseMessage responseMessage = await client.GetAsync(fullRequestUri);
            string responseStr = await responseMessage.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return result;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, TRequest request)
            where TRequest : IHTTPRequest
            where TResponse : IHTTPResponse
        {
            string jsonContent = JsonConvert.SerializeObject(request);
            StringContent content = new(jsonContent);
            HttpResponseMessage responseMessage = await client.PostAsync(requestUri, content);
            string responseStr = await responseMessage.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return result;
        }

        private string GetRequestString(IHTTPRequest request)
        {
            PropertyInfo[] properties = request.GetType().GetProperties();
            Dictionary<string, string> keyValuePairs = new();
            foreach (PropertyInfo pi in properties)
            {
                var value = pi.GetValue(request);
                keyValuePairs.Add(pi.Name, value.ToString());
            }

            return string.Join("&", keyValuePairs.Select(pair => $"{pair.Key}={pair.Value}"));
        }
    }
}
