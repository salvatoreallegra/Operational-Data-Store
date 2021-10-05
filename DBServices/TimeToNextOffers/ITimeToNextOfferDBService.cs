using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface ITimeToNextOfferDBService
    {
        Task<IEnumerable<TimeToNextOfferEntity>> GetMultipleAsync(string query);
        Task<TimeToNextOfferEntity> GetAsync(string id);
        Task AddAsync(TimeToNextOfferEntity item);
        Task<IEnumerable<TimeToNextOfferEntity>> getByMatchSequence(string query);
    }
}
