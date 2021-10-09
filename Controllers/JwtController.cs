using Microsoft.AspNetCore.Mvc;
using ODSApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Controllers
{
    [Route("donornet-analytics/v1/auth/")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        [HttpGet]
        public IActionResult Jwt()
        {
            return new ObjectResult(JwtToken.GenerateJwtToken());
        }
    }
}
