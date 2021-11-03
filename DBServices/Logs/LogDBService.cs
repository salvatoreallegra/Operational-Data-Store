/****************************************************
 * Database Service Class
 * Exposes methods for performing database operations
 * on Log Collection
 * *************************************************/

using Microsoft.Azure.Cosmos;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public class LogDBService : ILogDBService
    {
        private Container _container;
        public LogDBService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(LogEntity item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchID));
        }
       
        public async Task<LogEntity> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<LogEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<LogEntity>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<LogEntity>(new QueryDefinition(queryString));
            var results = new List<LogEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
       
    }
}
