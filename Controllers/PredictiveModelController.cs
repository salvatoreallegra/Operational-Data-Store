using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/predictivemodel")]
    public class PredictiveModelController : ControllerBase
    {
        private readonly IRepository repository;

        public PredictiveModelController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allmodels")]
        public ActionResult<List<PredictiveModel>> Get()
        {
            return repository.getAllModels();
        }
        
        [HttpGet("{CenterId:int}")]
        public ActionResult<List<PredictiveModel>> GetByCenterId(int CenterId)
        {
            var predictiveModel = repository.GetPredictiveModelsByCenterId(CenterId);
            if (predictiveModel == null)
            {
                return NotFound();

            }
            return predictiveModel;
        }
        [HttpPost]
        public ActionResult Post([FromBody] PredictiveModel model)
        {
            
              
            
             return NoContent(); //This is ok result to client, but returns nothing in the body
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
