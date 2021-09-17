using Microsoft.Azure.Cosmos;
using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class LogService : ILogService
    {
        private Microsoft.Azure.Cosmos.Container _container;
        public LogService(
          CosmosClient dbClient,
          string databaseName,
          string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddLogAsync(Log log)
        {
            await this._container.CreateItemAsync<Log>(log, new PartitionKey(log.Id));
        }
    }
}
