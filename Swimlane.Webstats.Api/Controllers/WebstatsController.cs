using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swimlane.Webstats.Dto.Request;
using Swimlane.Webstats.Dto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimlane.Webstats.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebstatsController : ControllerBase
    {
       
        private readonly ILogger<WebstatsController> _logger;


        public WebstatsController(ILogger<WebstatsController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        public IActionResult Get(RequestDto request)
        {
            if (request == null)
                return BadRequest();

            TryValidateModel(request);

            //validate request
            if (ModelState.IsValid)
                return BadRequest();


            IEnumerable<ServiceResultDto> result = new List<ServiceResultDto>();

            return Content(JsonConvert.SerializeObject(result));
        }
    }
}
