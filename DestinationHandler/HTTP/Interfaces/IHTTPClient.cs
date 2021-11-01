using System.Threading.Tasks;

namespace DestinationHandler.HTTP
{
    internal interface IHTTPClient
    {
        public Task<TResponse> GetAsync<TResponse>(string requestUri)
            where TResponse : IHTTPResponse;

        public Task<TResponse> PostAsync<TResponse>(string requestUri, IHTTPRequest request)
            where TResponse : IHTTPResponse;
    }
}
