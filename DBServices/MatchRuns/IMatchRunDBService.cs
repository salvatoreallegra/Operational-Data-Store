using ODSApi.DTOs;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public interface IMatchRunDBService
    {
        Task<IEnumerable<MatchRunEntity>> GetMultipleAsync(string query);
        Task<MatchRunEntity> GetAsync(string id);
        Task AddAsync(MatchRunCreateDto item);
        Task<IEnumerable<MatchRunEntity>> getByMatchSequence(string query);


    }
}
