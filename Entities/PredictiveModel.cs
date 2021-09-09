using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class PredictiveModel
    {
        public int Id { get; set; }
        public int MatchId { get; set; }  //predictive model match id from model api
        public int SequenceId { get; set; } //identifier of patient from model api
        public int AuditId { get; set; } //
        public int LogId { get; set; }

        public List<DataPoint> dataPoints { get; set; }

    }
}
