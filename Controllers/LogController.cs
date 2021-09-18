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
        private readonly IRepository _repository;
        
        public LogController(IRepository repository)
        {
            _repository = repository;
        }
     
        [HttpGet("{CenterId}/{MatchId}")]
        public ActionResult<List<Log>> GetAllLogsByCenterIdMatchId(int CenterId, int MatchId)
        {
            var log = _repository.getLogByCenterIdMatchId(CenterId, MatchId);
            if (log == null)
            {
                return NotFound();

            }
            return log;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateLogDTO _logDTO)
        {



            return NoContent(); //This is ok result to client, but returns nothing in the body
        }
    }
}
