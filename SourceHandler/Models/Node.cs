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
    }
}
