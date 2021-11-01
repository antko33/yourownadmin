using DestinationHandler.HTTP;
using System.Runtime.Serialization;

namespace DestinationHandler.Models
{
    [DataContract]
    class GetUpoadServerResponse : IHTTPResponse
    {
        [DataMember(Name = Constants.ResponsePropertyName)]
        public GetUploadServerResponseRoot Response { get; set; }
    }

    [DataContract]
    class GetUploadServerResponseRoot
    {
        [DataMember(Name = Constants.AlbumIdPropertyName)]
        public int AlbumId { get; set; }

        [DataMember(Name = Constants.UploadUrlPropertyName)]
        public string UploadUrl { get; set; }

        [DataMember(Name = Constants.UserIdPropertyName)]
        public string UserId { get; set; }
    }
}
