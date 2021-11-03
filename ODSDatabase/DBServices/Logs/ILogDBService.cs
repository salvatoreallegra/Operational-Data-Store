using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
{
    public interface ILogDBService
    {
        Task<IEnumerable<Log>> GetMultipleAsync(string query);
        Task<Log> GetAsync(string id);
        Task AddAsync(Log item);
        
        
    }
}
