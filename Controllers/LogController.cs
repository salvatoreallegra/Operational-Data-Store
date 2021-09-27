using Microsoft.AspNetCore.Mvc;
using ODSApi.DTOs;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogDBService _cosmosDbService;
        public LogController(ILogDBService cosmosDbService)
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
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LogEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        
    }
      
}
