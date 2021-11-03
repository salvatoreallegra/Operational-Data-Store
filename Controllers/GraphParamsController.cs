/**********************************************
 * Controller to create a graph params record
 * and get all graph params records
 * TBD how graph params will be added or modified
 * in the ODS database in future
 * *******************************************/
using Microsoft.AspNetCore.Mvc;
using ODSDatabase.DBServices;
using Model.Model;
using System;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/graphparams")]
    [ApiController]
    public class GraphParamsController : ControllerBase
    {
        private readonly IGraphParamsDBService _cosmosDbService;
        public GraphParamsController(IGraphParamsDBService cosmosDbService)
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
        public async Task<IActionResult> Create([FromBody] GraphParams item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

    }

}
