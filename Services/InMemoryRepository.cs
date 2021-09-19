using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<MatchRunEntity> _models;
        private List<Log> _logs;
        private List<TimeToBetterEntity> _timeToBetters;
        private List<MortalitySlopeEntity> _mortalitySlope;
        public InMemoryRepository()
        {
            _models = new List<MatchRunEntity>
            {
                new MatchRunEntity(){Id = 1, MatchId = 89098309, OfferDate = new DateTime(2020, 6, 1, 8, 30, 52), OfferKdpi = 2.49f,
                    SequenceId = 9837493, LogId = 00029378, CenterId= 4444,
                DonorAuditId = 88475934, WaitListId = 4782938, WaitListAuditId = 204968},
                    // dataPoints = new List<DataPoint>{ new DataPoint { dataPoint = "Dead in 6 months" }, new DataPoint {dataPoint = "Kidney not found"} } },
                //new MatchRunEntity(){Id = 2, MatchId = 89098309, Offer_Date = new DateTime(2021, 5, 1, 8, 30, 52), Offer_KDPI = .49f,
                //    Sequence_Id = 98565, Log_Id = 456345, CenterId = 4444,
                //Donor_Audit_id = 05453, WL_ID = 2342342, WL_Audit_ID = 1111111},
                //new MatchRunEntity(){Id = 3, MatchId = 794444, Offer_Date = new DateTime(2021, 9, 1, 8, 30, 52), Offer_KDPI = .56f,
                //    Sequence_Id = 91165, Log_Id = 956345, CenterId= 32224,
                //Donor_Audit_id = 05678, WL_ID = 33333, WL_Audit_ID = 222222}
            };
            _logs = new List<Log>
            {
                new Log {Id = "1", CenterId = 4444, DrName = "Dr. Who", MatchID = 89098309,  SequenceIds = new List<int>{9837493,4568403}, TimeStamp = new DateTime(2008, 5, 1, 8, 30, 52) },
                new Log {Id = "2", CenterId = 4444, DrName = "Dr. Who", MatchID = 89098309,  SequenceIds = new List<int>{9837493,4568403}, TimeStamp = new DateTime(2008, 5, 1, 8, 30, 52) }

            };
            _timeToBetters = new List<TimeToBetterEntity>
            {
                new TimeToBetterEntity{Id ="1", MatchId = 89098309, ModelVersionId = 777, SequenceId = 9837493,
                TimeStamp = new DateTime(2008, 5, 1, 8, 30, 52), TimeToBetter = new Dictionary<string,int>() { { "TimeToBetter30", 34 },{ "TimeToBetter50", 23 } }
                }
            };
            _mortalitySlope = new List<MortalitySlopeEntity>
            {
                new MortalitySlopeEntity
                {Id = 10,
                SequenceId = 87747,
                WaitListMortality = new Dictionary<string, float>(10) { {"Time", 5 }, { "Time", 8 }, { "Time", 2.4f }, { "Time", 8.4f }, { "Time", 8 },
                {"Time", 1 },{"Time", 5 },{"Time", 2 },{"Time", 4 },{"Time", 5 }} }
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
        public MortalitySlopeEntity GetMortalitySlopeBySequenceId(int sequenceId)
        {

            return _mortalitySlope.FirstOrDefault(x => x.Id == sequenceId);

        }
        public List<MatchRunEntity> getAllModels()
        {
            return _models;
        }
        public List<TimeToBetterEntity> getAllTimeToBetter()
        {
            return _timeToBetters;
        }

        public MatchRunEntity GetPredictiveModelById(int Id)
        {
            return _models.FirstOrDefault(x => x.Id == Id);
        }
        public List<MatchRunEntity> GetMatchRunRecordsByCenterIdMatchId(int centerId, int matchId)
        {
            List<MatchRunEntity> returnModels = new List<MatchRunEntity>();
            foreach (var model in _models)
            {
                if (model.CenterId == centerId && model.MatchId == matchId)
                {
                    returnModels.Add(model);
                }
            }
            return returnModels;
        }

    }
}
