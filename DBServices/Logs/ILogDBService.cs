using ODSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public interface ILogDBService
    {
        Task<IEnumerable<LogEntity>> GetMultipleAsync(string query);
        Task<LogEntity> GetAsync(string id);
        Task AddAsync(LogEntity item);
        
        
    }
}
