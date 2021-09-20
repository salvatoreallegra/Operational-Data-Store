using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IMatchRunService
    {
        Task<IEnumerable<MatchRunEntity>> GetMultipleAsync(string query);
        Task<MatchRunEntity> GetAsync(string id);
        Task AddAsync(MatchRunEntity item);
        Task<IEnumerable<MatchRunEntity>> getByMatchSequence(string query);


    }
}
