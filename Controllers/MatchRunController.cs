using Microsoft.AspNetCore.Mvc;
using ODSApi.BusinessServices;
using ODSApi.DBServices;
using ODSApi.DTOs;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ODSApi.Controllers
{
    [Route("donornet-analytics/v1/matches/")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunDBService _matchRunService;
        
        private readonly IMatchRunBusinessService _matchRunBusinessService;


        public MatchRunController(IMatchRunDBService matchRunService, IMatchRunBusinessService matchRunBusinessService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));            
            _matchRunBusinessService = matchRunBusinessService ?? throw new ArgumentNullException(nameof(matchRunBusinessService));
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _matchRunService.GetAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _matchRunService.GetMultipleAsync("SELECT * FROM c"));

        }

        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchRunCreateDto item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _matchRunService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpGet("{match_id}/potential-recepients/{PtrSequenceNumber}")]
        public async Task<IActionResult> GetByMatchSequence(int match_id, int PtrSequenceNumber)
        {
            var matchRunRecords = await _matchRunBusinessService.getByMatchSequence(match_id,PtrSequenceNumber);

            if(matchRunRecords.errors == ERRORS.NoPassThroughRecord)
            {
                return NotFound("No Pass Through Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            if (matchRunRecords.errors == ERRORS.Duplicates)
            {
                return NotFound("Duplicate Records found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            else if(matchRunRecords.errors == ERRORS.NoMortalitySlopeRecord){
                return NotFound("No Mortality Slope Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }
            else if(matchRunRecords.errors == ERRORS.DataValidationError)
            {
                return NoContent();
            }
            

            List<MatchRunEntity> returnEntity = matchRunRecords.Data;                    

            return Ok(returnEntity);

        }

       

    }
}
