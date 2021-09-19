using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IPassThroughService
    {
        List<MatchRunEntity> GetMatchRunRecordsByCenterIdMatchId(string centerId, int matchId);

    }
}
