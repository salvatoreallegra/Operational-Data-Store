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
        private readonly IRepository repository;

        public MatchRunController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<MatchRun>> Get()
        {
            return repository.getAllModels();
        }

        [HttpGet("{CenterId}/{MatchId}")]
        public ActionResult<List<MatchRun>> GetAllMatchRecordsByCenterIdMatchId(string CenterId, int MatchId)
        {

            var predictiveModel = repository.GetMatchRunRecordsByCenterIdMatchId(CenterId, MatchId);
              if(MatchId < 10) {
               

            }
            return predictiveModel;
        }
      
    }
}
