using DestinationHandler.HTTP;
using DestinationHandler.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DestinationHandler
{
    class Program
    {
        public static async Task Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");

            KenotHTTPClient client = new();

            string requestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.GetWallUploadServerMethod}?{Constants.AccessTokenPropertyName}={SecuredConstants.AccessToken}&{Constants.VersionPropertyName}={Constants.ApiVersion}";
            var response = await client.GetAsync<GetWallUpoadServerResponse>(requestUri);
            string uploadUrl = response.Response.UploadUrl;

            string filePath = @"C:\Users\antko\OneDrive\Pictures\uzILxZSc_N.jpg";
            var uploadResponse = await client.PostMediaAsync<MediaUploadResponse>(uploadUrl, filePath);
        }
    }
}
