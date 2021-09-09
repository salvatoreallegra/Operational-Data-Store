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
        public List<PredictiveModel> Get()
        {
            return repository.getAllModels();
        }
        [HttpGet("{id:int}")]
        public PredictiveModel Get(int id)
        {
            var predictiveModel = repository.GetPredictiveModelById(id);
            if(predictiveModel == null)
            {

            }
            return predictiveModel;
        }
        [HttpPost]
        public void Put()
        {

        }
        [HttpDelete]
        public void Delete()
        {

        }
    }
}
