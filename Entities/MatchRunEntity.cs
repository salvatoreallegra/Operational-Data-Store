using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class MatchRunEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //Id for record 

        [JsonProperty(PropertyName = "matchid")]
        public int MatchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; } //identifier of patient from model api

        [JsonProperty(PropertyName = "offerdate")]
        public DateTime OfferDate { get; set; }       

        [JsonProperty(PropertyName = "donorauditid")]
        public int DonorAuditId { get; set; } //Audit id sent from mobile client

        [JsonProperty(PropertyName = "centerid")]
        public int CenterId { get; set; }

        [JsonProperty(PropertyName = "logid")]
        public int LogId { get; set; }  //Log id sent from mobile client

        [JsonProperty(PropertyName = "waitlistid")]
        public int WaitListId { get; set; }

        [JsonProperty(PropertyName = "waitlistauditid")]
        public int WaitListAuditId { get; set; }
                      

        [JsonProperty(PropertyName = "plotpoints")]
        public List<Dictionary<string, float>> PlotPoints { get; set; }  //This is wait list mortality from Mortality Slope Collection


        [JsonProperty(PropertyName = "timetonext30")]
        public Dictionary<string, float> TimeToNext30 { get; set; }

        [JsonProperty(PropertyName = "timetonext50")]
        public Dictionary<string, float> TimeToNext50 { get; set; }

        //Add graph params
        [JsonProperty(PropertyName = "graphparams")]
        public List<float> GraphParams { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }







    }
    
    
}
