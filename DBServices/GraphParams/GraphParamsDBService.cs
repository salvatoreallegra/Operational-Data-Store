using Microsoft.Azure.Cosmos;
using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public class GraphParamsDBService : IGraphParamsDBService
    {

        private Container _container;

        public GraphParamsDBService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(GraphParamsEntity item)
        {
            // await _container.CreateItemAsync(item, new PartitionKey(item.Id));
             await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<GraphParamsEntity> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<GraphParamsEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<GraphParamsEntity>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<GraphParamsEntity>(new QueryDefinition(queryString));
            var results = new List<GraphParamsEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<GraphParamsEntity>> getByMatchSequence(string queryString)
        {
            try
            {
                var query = _container.GetItemQueryIterator<GraphParamsEntity>(new QueryDefinition(queryString));

                var results = new List<GraphParamsEntity>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (CosmosException)
            {
                return null;
            }
        }
    }
}
