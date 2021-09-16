using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class Log
    {
        public int Id { get; set; }

        [Required]
        public string DrName { get; set; }

        public int CenterId { get; set; }

        public int MatchID { get; set; }

        public List<int> SequenceIds { get; set; }

        public DateTime timeStamp { get; set; }
    }
}
