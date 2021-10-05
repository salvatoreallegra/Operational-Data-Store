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
        private readonly IMortalitySlopeDBService _mortalitySlopeService;
        private readonly ITimeToNextOfferDBService _timeToBetterService;
        private readonly IGraphParamsDBService _graphParamsDBService;
        private readonly IMatchRunBusinessService _matchRunBusinessService;


        public MatchRunController(IMatchRunDBService matchRunService, IMortalitySlopeDBService mortalitySlopeService, ITimeToNextOfferDBService timeToBetterService, IGraphParamsDBService graphParamsDBService, IMatchRunBusinessService matchRunBusinessService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _mortalitySlopeService = mortalitySlopeService ?? throw new ArgumentNullException(nameof(mortalitySlopeService));
            _timeToBetterService = timeToBetterService ?? throw new ArgumentNullException(nameof(timeToBetterService));
            _graphParamsDBService = graphParamsDBService ?? throw new ArgumentNullException(nameof(graphParamsDBService));
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
            if(matchRunRecords.ResponseCode == 1)
            {
                return NotFound("No Pass Through Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);

            }
            else if(matchRunRecords.ResponseCode == 2){
                return NotFound("No Mortality Slope Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }
            else if(matchRunRecords.ResponseCode == 3)
            {
                return StatusCode(209, "The mortality slope field is null");
            }
            else if (matchRunRecords.ResponseCode == 4)
            {
                return NotFound("No Time to Next Offer Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            List<MatchRunEntity> returnEntity = matchRunRecords.Data;         
           

            return Ok(returnEntity);

        }

       

    }
}
