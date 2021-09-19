using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface ITimeToBetterService
    {
        Task<IEnumerable<TimeToBetterEntity>> GetMultipleAsync(string query);
        Task<TimeToBetterEntity> GetAsync(string id);
        Task AddAsync(TimeToBetterEntity item);
    }
}
