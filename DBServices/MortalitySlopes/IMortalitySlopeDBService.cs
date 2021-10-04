using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IMortalitySlopeDBService
    {
        Task<IEnumerable<MortalitySlopeEntity>> GetMultipleAsync(string query);
        Task<MortalitySlopeEntity> GetAsync(string id);
        Task AddAsync(MortalitySlopeEntity item);

        Task<IEnumerable<MortalitySlopeEntity>> getByMatchSequence(string query);


    }
}
