/***************************************************
 * This is a throwaway controller
 * We are using this i development to create records
 * and view records for testing and debugging.
 * *************************************************/

using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.DBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/timetobetter")]
    [ApiController]
    public class TimeToBetterController : ControllerBase
    {
        private readonly ITimeToNextOfferDBService _cosmosDbService;
        public TimeToBetterController(ITimeToNextOfferDBService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }
        // GET api/items
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c"));
        }
        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            TimeToNextOfferEntity returnEntity = await _cosmosDbService.GetAsync(id);
             if(returnEntity.SequenceId == 0)
             {
                return StatusCode(209);
             }
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeToNextOfferEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
    }
}
