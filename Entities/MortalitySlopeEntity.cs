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

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "matchid")]
        public int MatchId { get; set; }  //predictive model match id from model api
       
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "modelversion")]
        public string ModelVersion { get; set; }

        [JsonProperty(PropertyName = "waitlistmortality")]
        public List<Dictionary<string,float>> WaitListMortality { get; set; }
        
         



    }
}
