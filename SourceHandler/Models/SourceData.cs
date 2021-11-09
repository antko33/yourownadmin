using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SourceHandler.Models
{
    [DataContract]
    class SourceData
    {
        [DataMember(Name = Constants.CountPropertyName)]
        public int Count { get; set; }

        [DataMember(Name = Constants.PageInfoPropertyName)]
        public PageInfo PageInfo { get; set; }

        [DataMember(Name = Constants.EdgesPropertyName)]
        public List<NodeWrapper> Edges { get; set; }
    }

    [DataContract]
    class PageInfo
    {
        [DataMember(Name = Constants.HasNextPagePropertyName)]
        public bool HasNextPage { get; set; }
    }

    [DataContract]
    class NodeWrapper
    {
        [DataMember]
        public Node Node { get; set; }
    }
}
