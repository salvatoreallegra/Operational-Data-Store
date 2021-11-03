using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace ODSApi.DBServices
{
    public interface ITimeToNextOfferDBService
    {
        Task<IEnumerable<TimeToNextOffer>> GetMultipleAsync(string query);
        Task<TimeToNextOffer> GetAsync(string id);
        Task AddAsync(TimeToNextOffer item);
        Task<IEnumerable<TimeToNextOffer>> getByMatchSequence(string query);
    }
}
