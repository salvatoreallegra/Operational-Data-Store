using Microsoft.Azure.Cosmos;
using ODSApi.DTOs;
using ODSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DBServices
{
    public class MatchRunDBService : IMatchRunDBService
    {
        private Container _container;
        public MatchRunDBService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
           )        
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
      
        public async Task AddAsync(MatchRunCreateDto item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchId));

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
                MatchRun.MortalitySlopePlotPoints = mortalitySlopePoints;
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
            try
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
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;                
            }
            
        }
    }
}
