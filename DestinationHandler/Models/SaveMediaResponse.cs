using CommonLibs.HTTP;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DestinationHandler.Models
{
    [DataContract]
    class SaveMediaResponse : IHTTPResponse
    {
        [DataMember(Name = Constants.ResponsePropertyName)]
        public List<SaveMediaResponseRoot> Response { get; set; }
    }

    [DataContract]
    class SaveMediaResponseRoot
    {
        [DataMember(Name = Constants.IdPropertyName)]
        public string Id { get; set; }

        [DataMember(Name = Constants.OwnerIdPropertyName)]
        public string OwnerId { get; set; }
    }
}
