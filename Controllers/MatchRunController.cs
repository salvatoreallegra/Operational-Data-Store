using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/matchrun")]
    public class MatchRunController : ControllerBase
    {
        private readonly IRepository repository;

        public MatchRunController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [HttpGet("/list")]
        [HttpGet("/allmodels")]
        public ActionResult<List<MatchRun>> Get()
        {
            return repository.getAllModels();
        }
        
        [HttpGet("{CenterId}")]
        public ActionResult<List<MatchRun>> GetByCenterId(string CenterId)
        {
            var predictiveModel = repository.GetPredictiveModelsByCenterId(CenterId);
            if (predictiveModel == null)
            {
                return NotFound();

            }
            return predictiveModel;
        }
        [HttpPost]
        public ActionResult Post([FromBody] MatchRun model)
        {
            
              
            
             return NoContent(); //This is ok result to client, but returns nothing in the body
        }
        //public ActionResult Post([FromBody] MatchRunModel model)
        //{



          //  return NoContent(); //This is ok result to client, but returns nothing in the body
        //}
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
