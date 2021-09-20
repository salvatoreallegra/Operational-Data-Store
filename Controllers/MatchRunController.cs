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
    [Route("api/matchrun")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunService _cosmosDbService;
        public MatchRunController(IMatchRunService cosmosDbService)
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
        [HttpGet("{MatchId}/{SequenceId}")]
        public async Task<IActionResult> GetByMatchSequence(int MatchId,int SequenceId)
        {
            return Ok(await _cosmosDbService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " +  SequenceId));
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchRunEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        //private readonly IRepository repository;

        //public MatchRunController(IRepository repository)
        //{
        //    this.repository = repository;
        //}

        //[HttpGet]
        //public ActionResult<List<MatchRunEntity>> Get()
        //{
        //    return repository.getAllModels();
        //}

        //[HttpGet("/timetobetter")]
        //public ActionResult<List<TimeToBetterEntity>> GetAllTimeToBetters()
        //{
        //    return repository.getAllTimeToBetter();
        //}

        //[HttpGet("{CenterId}/{MatchId}")]
        //public ActionResult<List<MatchRunEntity>> GetAllMatchRecordsByCenterIdMatchId(int CenterId, int MatchId)
        //{

        //    var predictiveModel = repository.GetMatchRunRecordsByCenterIdMatchId(CenterId, MatchId);
        //      if(MatchId < 10) {


        //    }
        //    return predictiveModel;
        //}

    }
}
