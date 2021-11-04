using Microsoft.Azure.Cosmos;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
{
    public class TimeToNextOfferDBService : ITimeToNextOfferDBService
    {
        private Container _container;
        public TimeToNextOfferDBService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(TimeToNextOffer item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchId));
        }

        public async Task<TimeToNextOffer> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<TimeToNextOffer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<TimeToNextOffer>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<TimeToNextOffer>(new QueryDefinition(queryString));
            var results = new List<TimeToNextOffer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<IEnumerable<TimeToNextOffer>> getByMatchSequence(string queryString)
        {

            var query = _container.GetItemQueryIterator<TimeToNextOffer>(new QueryDefinition(queryString));
            var results = new List<TimeToNextOffer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;

        }
                  
    }
}
