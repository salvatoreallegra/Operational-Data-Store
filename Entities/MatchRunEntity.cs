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
        public int Id { get; set; }  //Id for record 

        [JsonProperty(PropertyName = "matchid")]
        public int MatchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "offerdate")]
        public DateTime OfferDate { get; set; }

        [JsonProperty(PropertyName = "offerkdpi")]
        public float OfferKdpi { get; set; }

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; } //identifier of patient from model api

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


        //public int Kdpi { get; set; }  //kdpi value sent from mobile client

       // float[] dataPoints { get; set; } //2 datapoints known so far, time in days = 2.5, .3f is probability of survival
        
             

       // public List<DataPoint> dataPoints { get; set; }  // disregard for now

       
    }
    
    
}
