using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class MortalitySlopeEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "sequenceId")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "matchId")]
        public int MatchId { get; set; }  //predictive model match id from model api
       
        [JsonProperty(PropertyName = "timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "modelVersion")]
        public float ModelVersion { get; set; }

        [JsonProperty(PropertyName = "waitListMortality")]
        public List<Dictionary<string,float>> WaitListMortality { get; set; }
        
         



    }
}
