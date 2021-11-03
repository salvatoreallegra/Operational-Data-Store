using Microsoft.AspNetCore.Mvc;
using Model.Model;
using ODSDatabase.DBServices;
using System;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/mortalityslope")]
    [ApiController]
    public class MortalitySlopeController : ControllerBase
    {
        private readonly IMortalitySlopeDBService _cosmosDbService;
        public MortalitySlopeController(IMortalitySlopeDBService cosmosDbService)
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
        public async Task<IActionResult> Create([FromBody] MortalitySlope item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        
    }
      
}
