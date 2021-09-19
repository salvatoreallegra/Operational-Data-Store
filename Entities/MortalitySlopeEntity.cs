using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class MortalitySlopeEntity
    {
        public int Id { get; set; }
        public int SequenceId { get; set; }

        public Dictionary<string,float> WaitListMortality { get; set; } 


    }
}
