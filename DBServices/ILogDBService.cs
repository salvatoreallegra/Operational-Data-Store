using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface ILogDBService
    {
        Task<IEnumerable<Log>> GetMultipleAsync(string query);
        Task<Log> GetAsync(string id);
        Task AddAsync(Log item);
        
        
    }
}
