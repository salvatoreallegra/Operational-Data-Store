/**************************************************
Author: Salvatore Allegra
This class models a collection in CosmosDB,
it used by Entity Framework for database operations

**************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class TimeToBetterEntity
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int SequenceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ModelVersionId { get; set; }
        public Dictionary<string, int> TimeToBetter { get; set; }
                       
    }
}
