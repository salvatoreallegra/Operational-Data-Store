using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IMortalitySlopeService
    {
        Task<IEnumerable<MortalitySlopeEntity>> GetMultipleAsync(string query);
        Task<MortalitySlopeEntity> GetAsync(string id);
        Task AddAsync(MortalitySlopeEntity item);

        Task<IEnumerable<MortalitySlopeEntity>> getByMatchSequence(string query);
        //MortalitySlopeEntity getOneByMatchSequence(int MatchId, int SequenceId);


    }
}
