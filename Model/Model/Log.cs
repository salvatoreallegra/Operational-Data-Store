/**********************************************
 * Poco class that models the fields the ods
 * database will hold for the Logs Collection
 * and any other necessary computations related
 * to Logs
 * *******************************************/


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Log
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "matchId")]
        public int MatchID { get; set; }

        [JsonProperty(PropertyName = "sequenceNumber")]
        public int SequenceNumber { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
    }
}
