using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class Log
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "drname")]
        public string DrName { get; set; }

        [JsonProperty(PropertyName = "centerid")]
        public int CenterId { get; set; }
        [JsonIgnore]
        [JsonProperty(PropertyName = "matchid")]
        public int MatchID { get; set; }

        [JsonProperty(PropertyName = "sequenceids")]
        public List<int> SequenceIds { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }
    }
}
