using DestinationHandler.HTTP;
using System.Runtime.Serialization;

namespace DestinationHandler.Models
{
    [DataContract]
    class PostMediaResponse : IHTTPResponse
    {
        [DataMember(Name = Constants.ResponsePropertyName)]
        public PostMediaResponseRoot Response { get; set; }
    }

    [DataContract]
    class PostMediaResponseRoot
    {
        [DataMember(Name = Constants.PostIdPropertyName)]
        public string PostId { get; set; }
    }
}
