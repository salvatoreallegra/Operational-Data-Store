using ODSApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public interface IMortalitySlopeDBService
    {
        Task<IEnumerable<MortalitySlopeEntity>> GetMultipleAsync(string query);
        Task<MortalitySlopeEntity> GetAsync(string id);
        Task AddAsync(MortalitySlopeEntity item);

        Task<IEnumerable<MortalitySlopeEntity>> getByMatchSequence(string query);


    }
}
