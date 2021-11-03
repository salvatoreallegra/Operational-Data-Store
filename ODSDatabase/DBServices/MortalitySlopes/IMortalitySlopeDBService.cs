using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
{
    public interface IMortalitySlopeDBService
    {
        Task<IEnumerable<MortalitySlope>> GetMultipleAsync(string query);
        Task<MortalitySlope> GetAsync(string id);
        Task AddAsync(MortalitySlope item);

        Task<IEnumerable<MortalitySlope>> getByMatchSequence(string query);


    }
}
