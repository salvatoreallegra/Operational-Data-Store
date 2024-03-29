﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class MatchRunEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //Id for record 

        [JsonProperty(PropertyName = "matchId")]
        public int matchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "sequenceId")]
        public int SequenceId { get; set; } //identifier of patient from model api

        [JsonProperty(PropertyName = "offerdate")]
        public DateTime OfferDate { get; set; }

        
        [JsonProperty(PropertyName = "mortalitySlopePlotPoints")]
        public List<Dictionary<string, float>> MortalitySlopePlotPoints { get; set; }  //This is wait list mortality from Mortality Slope Collection


        [JsonProperty(PropertyName = "timetonext30")]
        public Dictionary<string, float> TimeToNext30 { get; set; }

        [JsonProperty(PropertyName = "timetonext50")]
        public Dictionary<string, float> TimeToNext50 { get; set; }

        //Add graph params
        [JsonProperty(PropertyName = "graphparams")]
        public GraphParamsEntity GraphParam { get; set; }

        [JsonProperty(PropertyName = "createddatetime")]
        public DateTime CreatedDateTime { get; set; }


    }
    
    
}
