using DestinationHandler.HTTP;
using DestinationHandler.Models;
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

            string requestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.GetUploadServerMethod}?{Constants.GroupIdPropertyName}={SecuredConstants.GroupId}&{Constants.AccessTokenPropertyName}={SecuredConstants.AccessToken}&{Constants.VersionPropertyName}={Constants.ApiVersion}";
            var response = await client.GetAsync<GetUpoadServerResponse>(requestUri);
            string uploadUrl = response.Response.UploadUrl;

            string filePath = @"https://images-api.intrepidgroup.travel/Peregrine/123909/8802845098014.jpg";
            var uploadResponse = await client.PostMediaAsync<MediaUploadResponse>(uploadUrl, filePath);

            string server = uploadResponse.Server;
            string photo = uploadResponse.Photo;
            string hash = uploadResponse.Hash;

            string saveRequestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.SaveMediaMethod}?{Constants.GroupIdPropertyName}={SecuredConstants.GroupId}&{Constants.ServerPropertyName}={server}&{Constants.PhotoPropertyName}={photo}&{Constants.HashPropertyName}={hash}&{Constants.AccessTokenPropertyName}={SecuredConstants.AccessToken}&{Constants.VersionPropertyName}={Constants.ApiVersion}";
            var saveResponse = await client.GetAsync<SaveMediaResponse>(saveRequestUri);

            var savedItems = saveResponse.Response;
            foreach (var item in savedItems)
            {
                string attachmentId = $"{Constants.PhotoPropertyName}{item.OwnerId}_{item.Id}";
                string postRequestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.PostMediaMethod}?{Constants.OwnerIdPropertyName}=-{SecuredConstants.GroupId}&from_group=1&close_comments=1&message=test&{Constants.AttachmentsPropertyName}={attachmentId}&{Constants.AccessTokenPropertyName}={SecuredConstants.AccessToken}&{Constants.VersionPropertyName}={Constants.ApiVersion}";
                var postResponse = await client.GetAsync<PostMediaResponse>(postRequestUri);
            }
        }
    }
}
