using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonLibs.HTTP
{
    public class KenotHTTPClient : IHTTPClient
    {
        private readonly HttpClient client = new();

        public async Task<TResponse> GetAsync<TResponse>(string requestUri)
            where TResponse : IHTTPResponse
        {
            HttpResponseMessage responseMessage = await client.GetAsync(requestUri);
            string responseStr = await responseMessage.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return result;
        }

        public async Task<string> GetRawAsync(string requestUri)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(requestUri);
            string responseStr = await responseMessage.Content.ReadAsStringAsync();
            return responseStr;
        }

        public async Task<TResponse> PostAsync<TResponse>(string requestUri, IHTTPRequest request)
            where TResponse : IHTTPResponse
        {
            string jsonContent = JsonConvert.SerializeObject(request);
            StringContent content = new(jsonContent);
            HttpResponseMessage responseMessage = await client.PostAsync(requestUri, content);
            string responseStr = await responseMessage.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return result;
        }

        public async Task<TResponse> PostMediaAsync<TResponse>(string requestUri, string fieldName, string sourceUrl)
            where TResponse : IHTTPResponse
        {
            MultipartFormDataContent formContent = new();
            formContent.Headers.ContentType.MediaType = "multipart/form-data";

            var responseStream = await GetResponseStream(sourceUrl);
            formContent.Add(new StreamContent(responseStream), fieldName, sourceUrl);

            HttpResponseMessage responseMessage = await client.PostAsync(requestUri, formContent);
            var responseStr = await responseMessage.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return result;
        }

        public async Task<Stream> GetResponseStream(string url)
        {
            var webRequest = WebRequest.Create(url);
            var response = await webRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            return responseStream;
        }
    }
}
