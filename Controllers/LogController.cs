/**********************************************
 * Controller to View Log records in ODS database
 * during development
 * *******************************************/


using Microsoft.AspNetCore.Mvc;
using ODSApi.Services;
using System;
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
      
    }      
}
