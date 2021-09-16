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
        public ActionResult<List<MatchRun>> GetAllMatchRecordsByCenterIdMatchId(string CenterId,int MatchId)
        {
            //try
            //{
            //    if (KDPI < 0)
            //        throw new Exception("KDPI Cannot be less than zero");
                var predictiveModel = repository.GetMatchRunRecordsByCenterIdMatchId(CenterId, MatchId);

                return predictiveModel;
            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

      


        //[HttpPost]
        //public ActionResult Post([FromBody] Log _log)
        //{
            
              
            
          //   return NoContent(); //This is ok result to client, but returns nothing in the body
        //}
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
