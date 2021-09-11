using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<PredictiveModel> _models;
        public InMemoryRepository()
        {
            _models = new List<PredictiveModel>
            {
                new PredictiveModel(){Id = 1, MatchId = 7898374, AuditId = 2193028, SequenceId = 9837493, LogId = 00029378, CenterId = 4444 },
                    // dataPoints = new List<DataPoint>{ new DataPoint { dataPoint = "Dead in 6 months" }, new DataPoint {dataPoint = "Kidney not found"} } },

                new PredictiveModel(){Id = 2, MatchId = 98746, AuditId = 3333, SequenceId = 775343, LogId = 01027378, CenterId = 22222 },
                     //dataPoints = new List<DataPoint>{ new DataPoint { dataPoint = "Cancer Victim" }, new DataPoint {dataPoint = "Cell Transplant"} } }
                new PredictiveModel(){Id = 2, MatchId = 98746, AuditId = 3223, SequenceId = 775343, LogId = 01344578, CenterId = 22222 }
            };
        }
        public List<PredictiveModel> getAllModels()
        {
            return _models;
        }

        public PredictiveModel GetPredictiveModelById(int Id)
        {
            return _models.FirstOrDefault(x => x.Id == Id);
        }
        public List<PredictiveModel> GetPredictiveModelsByCenterId(int centerId)
        {
            List<PredictiveModel> returnModels = new List<PredictiveModel>();
            foreach (var model in _models)
            {
                if(model.CenterId == centerId)
                {
                    returnModels.Add(model);
                }
            }
            return returnModels;
        }

    }
}
