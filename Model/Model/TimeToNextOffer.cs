/**************************************************
Author: Salvatore Allegra
This class models a collection in CosmosDB,
It used by Entity Framework for database operations.
It also enforced data types internally and in post and get requests
for time to next offer data
**************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model
{
    public class TimeToNextOffer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "matchId")]
        public int MatchId { get; set; }

        [JsonProperty(PropertyName = "sequenceId")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "modelVersionId")]
        public float ModelVersionId { get; set; }


        [JsonProperty(PropertyName = "timeToNext30")]
        public Dictionary<string, float> TimeToNext30 { get; set; }

        [JsonProperty(PropertyName = "timeToNext50")]
        public Dictionary<string, float> TimeToNext50 { get; set; }

    }
}
