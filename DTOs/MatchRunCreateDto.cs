using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DTOs
{
    public class MatchRunCreateDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //Id for record 

        [JsonProperty(PropertyName = "matchid")]
        public int MatchId { get; set; }  //predictive model match id from model api

        [JsonProperty(PropertyName = "sequenceid")]
        public int SequenceId { get; set; } //identifier of patient from model api

        [JsonProperty(PropertyName = "offerdate")]
        public DateTime OfferDate { get; set; }

        [JsonProperty(PropertyName = "institutionid")]
        public int InstitutionId { get; set; }

        [JsonProperty(PropertyName = "donorauditid")]
        public int DonorAuditId { get; set; } //Audit id sent from mobile client

        [JsonProperty(PropertyName = "createddatetime")]
        public DateTime createdDateTime { get; set; } //Audit id sent from mobile client

        [JsonProperty(PropertyName = "logid")]
        public int LogId { get; set; }  //Log id sent from mobile client

        [JsonProperty(PropertyName = "waitlistid")]
        public int WaitListId { get; set; }

        [JsonProperty(PropertyName = "waitlistauditid")]
        public int WaitListAuditId { get; set; }
    }
}
