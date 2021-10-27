﻿/**********************************************
 * Poco class that models the fields the ods
 * database will hold for the Graph Params Collection
 * and any other necessary computations related
 * to Graph Parameters
 * *******************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Models
{
    public class GraphParamsEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "xupper")]
        public int XUpper { get; set; }

        [JsonProperty(PropertyName = "xlower")]
        public int XLower { get; set; }

        [JsonProperty(PropertyName = "yupper")]
        public int YUpper { get; set; }

        [JsonProperty(PropertyName = "ylower")]
        public int YLower { get; set; }

        [JsonProperty(PropertyName = "timeunits")]
        public String TimeUnits { get; set; }

        [JsonProperty(PropertyName = "displayUnite")]
        public String DisplayUnits { get; set; }

        [JsonProperty(PropertyName = "createddatetime")]
        public DateTime createdDateTime { get; set; }

    }
}
