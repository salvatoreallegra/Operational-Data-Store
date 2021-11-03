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

namespace ODSDatabase.DBServices
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
        public async Task AddAsync(Log item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchID));
        }
       
        public async Task<Log> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Log>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Log>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Log>(new QueryDefinition(queryString));
            var results = new List<Log>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
       
    }
}
