using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("api/[controller]")]
    public class PredictiveModelController : Controller
    {
        private readonly IRepository repository;

        public PredictiveModelController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public List<PredictiveModel> Get()
        {
            return repository.getAllModels();
        }
    }
}
