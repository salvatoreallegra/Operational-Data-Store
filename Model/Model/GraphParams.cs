using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model
{
    public class GraphParams
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "xUpper")]
        public int XUpper { get; set; }

        [JsonProperty(PropertyName = "xLower")]
        public int XLower { get; set; }

        [JsonProperty(PropertyName = "yUpper")]
        public int YUpper { get; set; }

        [JsonProperty(PropertyName = "yLower")]
        public int YLower { get; set; }

        [JsonProperty(PropertyName = "timeUnits")]
        public String TimeUnits { get; set; }

        [JsonProperty(PropertyName = "displayUnits")]
        public String DisplayUnits { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTime createdDateTime { get; set; }
    }
}
