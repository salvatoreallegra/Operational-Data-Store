using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IRepository
    {
        List<MatchRun> getAllModels();
        MatchRun GetPredictiveModelById(int Id);

        List<MatchRun> GetPredictiveModelsByCenterId(string centerId);
    }
}
