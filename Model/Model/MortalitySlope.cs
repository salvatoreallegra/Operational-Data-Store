﻿/**********************************************
 * Poco class that models the fields the ods
 * database will hold for the Mortality Slope Collections
 * and any other necessary computations related
 * to Mortality Slope
 * *******************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Model.Model
{
    public class MortalitySlope
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "matchId")]
        public double MatchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "sequenceNumber")]
        public double SequenceNumber { get; set; }        

        [JsonProperty(PropertyName = "offerDate")]
        public DateTime OfferDate { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "modelVersion")]
        public string ModelVersion { get; set; }

        [JsonProperty(PropertyName = "waitListMortality")]
        public List<Dictionary<string, float>> WaitListMortality { get; set; }

    }
}