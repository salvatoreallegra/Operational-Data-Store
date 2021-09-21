using Microsoft.Azure.Cosmos;
using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public class MatchRunService : IMatchRunService
    {
        private Container _container;
        private readonly IMatchRunService matchRunService;
        public MatchRunService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(MatchRunEntity item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<MatchRunEntity> GetAsync(string id)
        {
            try
            {
                //get mortality slope by match id and sequence id 
                //
                MortalitySlopeEntity mortalitySlope = await _container.ReadItemAsync<MortalitySlopeEntity>(id, new PartitionKey(id));
                List<Dictionary<string, float>> mortalitySlopePoints = mortalitySlope.WaitListMortality;
                MatchRunEntity MatchRun = await _container.ReadItemAsync<MatchRunEntity>(id, new PartitionKey(id));
                MatchRun.PlotPoints = mortalitySlopePoints;
                return MatchRun;
                //return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<MatchRunEntity>> getByMatchSequence(string queryString)
        {

            var query = _container.GetItemQueryIterator<MatchRunEntity>(new QueryDefinition(queryString));
            var results = new List<MatchRunEntity>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;

        }
        public async Task<IEnumerable<MatchRunEntity>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<MatchRunEntity>(new QueryDefinition(queryString));
            var results = new List<MatchRunEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
