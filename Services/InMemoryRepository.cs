using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<MatchRun> _models;
        private List<Log> _logs;
        public InMemoryRepository()
        {
            _models = new List<MatchRun>
            {
                new MatchRun(){Id = 1, MatchId = 89098309, Offer_Date = "2021-01-01", Offer_KDPI = 2.49f,
                    Sequence_Id = "9837493", Log_Id = "00029378", CenterId= "4444",
                Donor_Audit_id = "88475934", WL_ID = "4782938", WL_Audit_ID = "204968"},
                    // dataPoints = new List<DataPoint>{ new DataPoint { dataPoint = "Dead in 6 months" }, new DataPoint {dataPoint = "Kidney not found"} } },
                new MatchRun(){Id = 2, MatchId = 89098309, Offer_Date = "2021-02-03", Offer_KDPI = .49f,
                    Sequence_Id = "98565", Log_Id = "456345", CenterId = "4444",
                Donor_Audit_id = "05453", WL_ID = "2342342", WL_Audit_ID = "1111111"},
                new MatchRun(){Id = 3, MatchId = 794444, Offer_Date = "2019-02-03", Offer_KDPI = .56f,
                    Sequence_Id = "91165", Log_Id = "956345", CenterId= "32224",
                Donor_Audit_id = "05678", WL_ID = "33333", WL_Audit_ID = "222222"}
            };
            _logs = new List<Log>
            {
                new Log {Id = 1, CenterId = 4444, DrName = "Dr. Who", MatchID = 89098309,  SequenceIds = new List<int>{9837493,4568403}, TimeStamp = new DateTime(2008, 5, 1, 8, 30, 52) },
                new Log {Id = 1, CenterId = 4444, DrName = "Dr. Who", MatchID = 89098309,  SequenceIds = new List<int>{9837493,4568403}, TimeStamp = new DateTime(2008, 5, 1, 8, 30, 52) }

            };
        }
        public List<Log> getLogByCenterIdMatchId(int centerId, int matchId)
        {

            List<Log> returnLogs = new List<Log>();
            foreach (var log in _logs)
            {
                if (log.CenterId == centerId && log.MatchID == matchId)
                {
                    returnLogs.Add(log);
                }
            }
            return returnLogs;
            
        }
        public List<MatchRun> getAllModels()

        {
            return _models;
        }

        public MatchRun GetPredictiveModelById(int Id)
        {
            return _models.FirstOrDefault(x => x.Id == Id);
        }
        public List<MatchRun> GetMatchRunRecordsByCenterIdMatchId(string centerId, int matchId)
        {
            List<MatchRun> returnModels = new List<MatchRun>();
            foreach (var model in _models)
            {
                if(model.CenterId.Equals(centerId) && model.MatchId == matchId)
                {
                    returnModels.Add(model);
                }
            }
            return returnModels;
        }

    }
}
