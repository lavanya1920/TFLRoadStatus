using System.Runtime.Serialization;

namespace TFLRoadStatus.API.Models
{
    public class RoadStatus
    {
        public bool IsSuccess { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "statusSeverity")]
        public string StatusSeverity { get; set; }

        [DataMember(Name = "statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }
        
    }
}
