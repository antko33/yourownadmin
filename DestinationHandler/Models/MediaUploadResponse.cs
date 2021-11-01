using DestinationHandler.HTTP;
using System.Runtime.Serialization;

namespace DestinationHandler.Models
{
    [DataContract]
    class MediaUploadResponse : IHTTPResponse
    {
        [DataMember(Name = Constants.ServerPropertyName)]
        public string Server { get; set; }

        [DataMember(Name = Constants.PhotoPropertyName)]
        public string Photo { get; set; }

        [DataMember(Name = Constants.HashPropertyName)]
        public string Hash { get; set; }
    }
}
