using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ODSApi.Helpers;

namespace ODSApi.Controllers
{
    [Route("api/matchrun")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunService _matchRunService;
        private readonly IMortalitySlopeService _mortalitySlopeService;
        //private OdSHelper _helper;
        
        public MatchRunController(IMatchRunService matchRunService, IMortalitySlopeService mortalitySlopeService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _mortalitySlopeService = mortalitySlopeService ?? throw new ArgumentNullException(nameof(mortalitySlopeService));
            //_helper = helper;
        }
        // GET api/items
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _matchRunService.GetMultipleAsync("SELECT * FROM c"));
        }
        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _matchRunService.GetAsync(id));
        }
        [HttpGet("{MatchId}/{SequenceId}")]
        public async Task<IActionResult> GetByMatchSequence(int MatchId,int SequenceId)
        {
                //var testSlope = _mortalitySlopeService.getOneByMatchSequence(MatchId, SequenceId);
                //        
                var matchRunRecords = await _matchRunService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);

                if (matchRunRecords.Count() == 0)
                {
                    return NotFound("No Records Found");
                }
                var mortalitySlopeRecords = await _mortalitySlopeService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);
                List<Dictionary<string,float>> plotpoints = null;
                foreach( var m in mortalitySlopeRecords)
                {
                   plotpoints = m.WaitListMortality;

                }
                foreach( var x in matchRunRecords)
                {
                    x.PlotPoints = plotpoints;
                }

            //Calculate Model Used
            float _kdpi;
            foreach (var x in matchRunRecords)
            {
                _kdpi = x.OfferKdpi;
            }



            //foreach (var x in matchRunRecords)
            //{
            //    x.ModelUsed = ;
            //}


            return Ok(matchRunRecords);
            //  return Ok(await _matchRunService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId));
              
      
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchRunEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _matchRunService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        

    }
}
