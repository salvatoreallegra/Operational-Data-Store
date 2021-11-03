using Microsoft.Azure.Cosmos;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
{
    public class MortalitySlopeDBService : IMortalitySlopeDBService
    {
        private Container _container;
        public MortalitySlopeDBService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(MortalitySlope item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchId));
        }

        public async Task<MortalitySlope> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<MortalitySlope>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<MortalitySlope>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<MortalitySlope>(new QueryDefinition(queryString));
            var results = new List<MortalitySlope>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<MortalitySlope>> getByMatchSequence(string queryString)
        {

            var query = _container.GetItemQueryIterator<MortalitySlope>(new QueryDefinition(queryString));
            var results = new List<MortalitySlope>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;

        }

    }
  }

