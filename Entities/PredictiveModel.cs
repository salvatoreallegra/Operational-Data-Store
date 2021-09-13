using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class PredictiveModel
    {
        public int Id { get; set; }  //Id for record stored in Database
        public int MatchId { get; set; }  //predictive model match id from model api
        public int SequenceId { get; set; } //identifier of patient from model api
        public int AuditId { get; set; } //Audit id sent from mobile client
        public int LogId { get; set; }  //Log id sent from mobile client  
        public int Kdpi { get; set; }  //kdpi value sent from mobile client

        float[] dataPoints { get; set; } //2 datapoints known so far, time in days = 2.5, .3f is probability of survival
        public int CenterId { get; set; }

     

       // public List<DataPoint> dataPoints { get; set; }  // disregard for now

       
    }
    enum kdpitouse
    {


    }
    
}
