using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface ILogService
    {
        //Task<IEnumerable<Log>> GetLogsAsync(string query);
        
        Task AddLogAsync(Log log);
    }
}
