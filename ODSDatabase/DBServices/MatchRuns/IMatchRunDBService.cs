using Model.DTOs;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
{
    public interface IMatchRunDBService
    {
        Task<IEnumerable<MatchRun>> GetMultipleAsync(string query);
        Task<MatchRun> GetAsync(string id);
        Task AddAsync(MatchRunCreateDto item);
        Task<IEnumerable<MatchRun>> getByMatchSequence(string query);


    }
}
