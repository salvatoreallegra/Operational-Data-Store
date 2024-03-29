﻿using Microsoft.Azure.Cosmos;
using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
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
        public async Task AddAsync(TimeToNextOfferEntity item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.MatchId));
        }

        public async Task<TimeToNextOfferEntity> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<TimeToNextOfferEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<TimeToNextOfferEntity>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<TimeToNextOfferEntity>(new QueryDefinition(queryString));
            var results = new List<TimeToNextOfferEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<IEnumerable<TimeToNextOfferEntity>> getByMatchSequence(string queryString)
        {

            var query = _container.GetItemQueryIterator<TimeToNextOfferEntity>(new QueryDefinition(queryString));
            var results = new List<TimeToNextOfferEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;

        }
        //public async Task<TimeToBetterEntity> getByMatchSequenceLinq(int matchId, int sequenceId)
        //{

        //   var person = await _container.GetItemLinqQueryable<TimeToBetterEntity>(true)
        //                        .Where(p => p.MatchId == matchId && p.SequenceId == sequenceId)
        //                        .ToList().First();

        //    return person;
        //}




            
    }
}
