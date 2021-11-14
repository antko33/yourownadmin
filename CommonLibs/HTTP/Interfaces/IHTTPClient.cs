using System.Threading.Tasks;

namespace CommonLibs.HTTP
{
    public interface IHTTPClient
    {
        Task<TResponse> GetAsync<TResponse>(string requestUri)
            where TResponse : IHTTPResponse;

        Task<string> GetRawAsync(string requestUri);

        Task<TResponse> PostAsync<TResponse>(string requestUri, IHTTPRequest request)
            where TResponse : IHTTPResponse;

        Task<TResponse> PostMediaAsync<TResponse>(string requestUri, string sourceUrl)
            where TResponse : IHTTPResponse;
    }
}
