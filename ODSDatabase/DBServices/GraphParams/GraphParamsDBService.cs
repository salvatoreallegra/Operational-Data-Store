/****************************************************
 * Database Service Class
 * Exposes methods for performing database operations
 * *************************************************/

using Microsoft.Azure.Cosmos;
using Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSDatabase.DBServices
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
        public async Task AddAsync(GraphParams item)
        {
             await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<GraphParams> GetAsync(string id)
        {            
                var response = await _container.ReadItemAsync<GraphParams>(id, new PartitionKey(id));
                return response.Resource;            
        }
        public async Task<IEnumerable<GraphParams>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<GraphParams>(new QueryDefinition(queryString));
            var results = new List<GraphParams>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<GraphParams>> getByMatchSequence(string queryString)
        {
                var query = _container.GetItemQueryIterator<GraphParams>(new QueryDefinition(queryString));

                var results = new List<GraphParams>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
                return results;
                        
        }
    }
}
