using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class MatchRun
    {
        public int Id { get; set; }  //Id for record 
        public int MatchId { get; set; }  //predictive model match id from model api
        public DateTime Offer_Date { get; set; }
        public float Offer_KDPI { get; set; }
        public int Sequence_Id { get; set; } //identifier of patient from model api
        public int Donor_Audit_id { get; set; } //Audit id sent from mobile client

        public int CenterId { get; set; }
        public int Log_Id { get; set; }  //Log id sent from mobile client
        public int WL_ID { get; set; }

        public int WL_Audit_ID { get; set; }


        //public int Kdpi { get; set; }  //kdpi value sent from mobile client

       // float[] dataPoints { get; set; } //2 datapoints known so far, time in days = 2.5, .3f is probability of survival
        
             

       // public List<DataPoint> dataPoints { get; set; }  // disregard for now

       
    }
    enum kdpitouse
    {


    }
    
}
