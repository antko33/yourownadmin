using DestinationHandler.HTTP;
using System.Runtime.Serialization;

namespace DestinationHandler.Models
{
    [DataContract]
    class GetWallUpoadServerResponse : IHTTPResponse
    {
        [DataMember(Name = Constants.ResponsePropertyName)]
        public GetWallUploadServerResponseRoot Response { get; set; }
    }

    [DataContract]
    class GetWallUploadServerResponseRoot
    {
        [DataMember(Name = Constants.AlbumIdPropertyName)]
        public int AlbumId { get; set; }

        [DataMember(Name = Constants.UploadUrlPropertyName)]
        public string UploadUrl { get; set; }

        [DataMember(Name = Constants.UserIdPropertyName)]
        public string UserId { get; set; }
    }
}
