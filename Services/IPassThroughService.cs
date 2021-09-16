using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IPassThroughService
    {
        List<MatchRun> GetMatchRunRecordsByCenterIdMatchId(string centerId, int matchId);

    }
}
