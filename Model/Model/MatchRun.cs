﻿/**********************************************
 * Poco class that models the fields the ods
 * database will hold for Pass Through Data
 * and any other necessary computations related
 * to Pass Through calculations
 * *******************************************/


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model
{
    public class MatchRun
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //Id for record 

        [JsonProperty(PropertyName = "matchId")]
        public double MatchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "sequenceNumber")]
        public double SequenceNumber { get; set; } //identifier of patient from model api

        [JsonProperty(PropertyName = "offerDate")]
        public DateTime OfferDate { get; set; }

        [JsonProperty(PropertyName = "mortalitySlopePlotPoints")]
        public List<Dictionary<string, float>> MortalitySlopePlotPoints { get; set; }  //This is wait list mortality from Mortality Slope Collection

        [JsonProperty(PropertyName = "timeToNext30")]
        public Dictionary<string, float> TimeToNext30 { get; set; }

        [JsonProperty(PropertyName = "timeToNext50")]
        public Dictionary<string, float> TimeToNext50 { get; set; }

        //Add graph params
        [JsonProperty(PropertyName = "graphparams")]
        public GraphParams GraphParam { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
    }
}