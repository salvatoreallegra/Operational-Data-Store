using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IRepository
    {
        List<MatchRunEntity> getAllModels();
        MatchRunEntity GetPredictiveModelById(int Id);

        List<MatchRunEntity> GetMatchRunRecordsByCenterIdMatchId(int centerId, int matchId);

        List<Log> getLogByCenterIdMatchId(int centerId, int matchID);

        List<TimeToBetterEntity> getAllTimeToBetter();

        MortalitySlopeEntity GetMortalitySlopeBySequenceId(int sequenceId);
    }
}
