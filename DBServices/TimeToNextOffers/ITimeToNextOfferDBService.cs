using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace ODSApi.DBServices
{
    public interface ITimeToNextOfferDBService
    {
        Task<IEnumerable<TimeToNextOfferEntity>> GetMultipleAsync(string query);
        Task<TimeToNextOfferEntity> GetAsync(string id);
        Task AddAsync(TimeToNextOfferEntity item);
        Task<IEnumerable<TimeToNextOfferEntity>> getByMatchSequence(string query);
    }
}
