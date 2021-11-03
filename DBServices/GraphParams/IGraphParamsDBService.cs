using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public interface IGraphParamsDBService
    {
        Task<IEnumerable<GraphParams>> GetMultipleAsync(string query);
        Task<GraphParams> GetAsync(string id);
        Task AddAsync(GraphParams item);
        Task<IEnumerable<GraphParams>> getByMatchSequence(string query);
    }
}
