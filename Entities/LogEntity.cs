using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class LogEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
                
        [JsonProperty(PropertyName = "matchid")]
        public int MatchID { get; set; }

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "createddatetime")]
        public DateTime CreatedDateTime { get; set; }
    }
}
