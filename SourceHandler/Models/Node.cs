using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SourceHandler.Models
{
    [DataContract]
    class Node
    {
        [DataMember(Name = Constants.ShortCodePropertyName)]
        public string ShortCode { get; set; }

        [DataMember(Name = Constants.IsVideoPropertyName)]
        public bool IsVideo { get; set; }

        [DataMember(Name = Constants.AccessibilityCaptionPropertyName)]
        public string AccessibilityCaption { get; set; }

        [DataMember(Name = Constants.DisplayUrlPropertyName)]
        public string DisplayUrl { get; set; }

        [DataMember(Name = SecuredConstants.ChildrenPropertyName)]
        public Children Children { get; set; }
    }

    [DataContract]
    class Children
    {
        [DataMember(Name = Constants.EdgesPropertyName)]
        public List<NodeWrapper> Edges { get; set; }
    }
}
