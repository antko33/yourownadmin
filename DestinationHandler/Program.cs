using DestinationHandler.HTTP;
using DestinationHandler.Models;
using System;
using System.Threading.Tasks;

namespace DestinationHandler
{
    class Program
    {
        public static async Task Main()
        {
            KenotHTTPClient client = new();

            string requestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.GetWallUploadServerMethod}?{Constants.AccessTokenPropertyName}={SecuredConstants.AccessToken}&{Constants.VersionPropertyName}={Constants.ApiVersion}";
            var response = await client.GetAsync<GetWallUpoadServerResponse>(requestUri);
            Console.WriteLine(response.Response.UploadUrl);
        }
    }
}
