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
    public class TimeToNextOfferEntity
    {
        [JsonProperty(PropertyName = "id")] 
        public string Id { get; set; }

        [JsonProperty(PropertyName = "matchId")]
        public int MatchId { get; set; }

        [JsonProperty(PropertyName = "sequenceId")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "modelversionid")]
        public float ModelVersionId { get; set; }

       
        [JsonProperty(PropertyName = "timetonext30")]
        public Dictionary<string, float> TimeToNext30 { get; set; }

        [JsonProperty(PropertyName = "timetonext50")]
        public Dictionary<string, float> TimeToNext50 { get; set; }

    }
}
