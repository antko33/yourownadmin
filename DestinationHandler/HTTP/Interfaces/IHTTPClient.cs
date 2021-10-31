using System.Threading.Tasks;

namespace DestinationHandler.HTTP
{
    internal interface IHTTPClient
    {
        public Task<TResponse> GetAsync<TRequest, TResponse>(string requestUri, IHTTPRequest request)
            where TRequest : IHTTPRequest
            where TResponse : IHTTPResponse;

        public Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, IHTTPRequest request)
            where TRequest : IHTTPRequest
            where TResponse : IHTTPResponse;
    }
}
