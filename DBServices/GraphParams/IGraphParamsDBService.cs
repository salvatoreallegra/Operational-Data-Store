using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public interface IGraphParamsDBService
    {
        Task<IEnumerable<GraphParamsEntity>> GetMultipleAsync(string query);
        Task<GraphParamsEntity> GetAsync(string id);
        Task AddAsync(GraphParamsEntity item);
        Task<IEnumerable<GraphParamsEntity>> getByMatchSequence(string query);
    }
}
