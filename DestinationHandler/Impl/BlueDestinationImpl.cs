using CommonLibs.HTTP;
using CommonLibs.Interfaces;
using DestinationHandler.Data;
using DestinationHandler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationHandler
{
    internal class BlueDestinationImpl : IDestinationUploader
    {
        private readonly KenotHTTPClient client = new();

        public async Task Upload(string sourceUrl)
        {
            string uploadUrl = await GetUploadUrl();
            var photo = await GetPhotoData(uploadUrl, sourceUrl);
            var saveResponse = await GetSaveResponse(photo);
            var savedItems = saveResponse.Response;

            string postRequestUri = $"{SecuredConstants.ApiRoot}{SecuredConstants.PostMediaMethod}";
            foreach (var item in savedItems)
            {
                string attachmentId = $"{Constants.PhotoPropertyName}{item.OwnerId}_{item.Id}";
                var param = new Dictionary<string, string>
                {
                    { Constants.OwnerIdPropertyName, $"-{SecuredConstants.GroupId}" },
                    { Constants.FromGroupPropertyName, "1" },
                    { Constants.CloseCommentsPropertyName,"1" },
                    { Constants.AttachmentsPropertyName, attachmentId },
                    { Constants.AccessTokenPropertyName, SecuredConstants.AccessToken },
                    { Constants.VersionPropertyName, Constants.ApiVersion }
                };
                await client.GetAsync<PostMediaResponse>(GetUrlWithParameters(postRequestUri, param));
            }
        }

        private string GetUrlWithParameters(string url, Dictionary<string, string> parameters)
        {
            StringBuilder result = new(url);

            if (parameters != null && parameters.Any())
            {
                result.Append("?");
                var paramString = string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"));
                result.Append(paramString);
            }

            return result.ToString();
        }

        private async Task<string> GetUploadUrl()
        {
            string requestUrl = $"{SecuredConstants.ApiRoot}{SecuredConstants.GetUploadServerMethod}";
            var param = new Dictionary<string, string>
            {
                { Constants.GroupIdPropertyName, SecuredConstants.GroupId },
                { Constants.AccessTokenPropertyName, SecuredConstants.AccessToken },
                { Constants.VersionPropertyName, Constants.ApiVersion }
            };
            string requestUri = GetUrlWithParameters(requestUrl, param);
            var response = await client.GetAsync<GetUpoadServerResponse>(requestUri);
            string uploadUrl = response.Response.UploadUrl;
            return uploadUrl;
        }

        private async Task<PhotoData> GetPhotoData(string uploadUrl, string sourceUrl)
        {
            var uploadResponse = await client.PostMediaAsync<MediaUploadResponse>(uploadUrl, sourceUrl);

            string server = uploadResponse.Server;
            string photo = uploadResponse.Photo;
            string hash = uploadResponse.Hash;

            return new PhotoData
            {
                Server = server,
                Hash = hash,
                Photo = photo
            };
        }

        private async Task<SaveMediaResponse> GetSaveResponse(PhotoData photo)
        {
            string requestUrl = $"{SecuredConstants.ApiRoot}{SecuredConstants.SaveMediaMethod}";
            var param = new Dictionary<string, string>
            {
                { Constants.GroupIdPropertyName, SecuredConstants.GroupId },
                { Constants.ServerPropertyName, photo.Server },
                { Constants.PhotoPropertyName, photo.Photo },
                { Constants.HashPropertyName, photo.Hash },
                { Constants.AccessTokenPropertyName, SecuredConstants.AccessToken },
                { Constants.VersionPropertyName, Constants.ApiVersion }
            };
            string saveRequestUri = GetUrlWithParameters(requestUrl, param);
            var saveResponse = await client.GetAsync<SaveMediaResponse>(saveRequestUri);
            return saveResponse;
        }
    }
}
