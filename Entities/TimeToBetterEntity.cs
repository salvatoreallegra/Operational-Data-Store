/**************************************************
Author: Salvatore Allegra
This class models a collection in CosmosDB,
it used by Entity Framework for database operations

**************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class TimeToBetterEntity
    {
        [JsonProperty(PropertyName = "id")] 
        public string Id { get; set; }

        [JsonProperty(PropertyName = "matchid")]
        public int MatchId { get; set; }

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "modelversionid")]
        public int ModelVersionId { get; set; }

        [JsonProperty(PropertyName = "timetobetter")]
        public Dictionary<string, int> TimeToBetter { get; set; }
                       
    }
}
