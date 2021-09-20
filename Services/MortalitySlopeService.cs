using Microsoft.Azure.Cosmos;
using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class MortalitySlopeService : IMortalitySlopeService
    {
        private Container _container;
        public MortalitySlopeService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(MortalitySlopeEntity item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<MortalitySlopeEntity> GetAsync(string id)
        {
            try
            {
                //var response = _cosmosDbService.GetMultipleAsync("SELECT * FROM c")
                var response = await _container.ReadItemAsync<MortalitySlopeEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<MortalitySlopeEntity>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<MortalitySlopeEntity>(new QueryDefinition(queryString));
            var results = new List<MortalitySlopeEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
