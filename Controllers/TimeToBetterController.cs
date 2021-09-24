using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
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
        private readonly ITimeToNextOffer _cosmosDbService;
        public TimeToBetterController(ITimeToNextOffer cosmosDbService)
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
            TimeToBetterEntity returnEntity = await _cosmosDbService.GetAsync(id);
             if(returnEntity.SequenceId == 0)
             {
                return StatusCode(209);
             }
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeToBetterEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
    }
}
