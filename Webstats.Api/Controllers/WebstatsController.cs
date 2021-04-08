using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Webstats.Api.Controllers.Base;
using Webstats.BaseServices;
using Webstats.Common;
using Webstats.Dto.Request;
using Webstats.Dto.Services;
using Webstats.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Webstats.Service.Controllers
{
    // TODO:
    // Handle Global Exceptions 
    // Handle Exceptions in worker +++
    // Add Data Parsers
    // Complete Tests
    // Accept IP 
    // Make sure service handle if worker is down +++
    // make swagger available after publish
    //
    [ApiController]
    [Route("[controller]")]
    public class WebstatsController : ApiBaseController
    {

        private readonly InfoCollector _infoCollector;


        public WebstatsController(
            ILogger<WebstatsController> logger,
            IConfiguration config,
            InfoCollector infoCollector) : base(logger, config)
        {
            _infoCollector = infoCollector;
        }


        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/api/v1/webstats")]
        public async Task<IActionResult> Get([FromQuery] string Host, [FromQuery] IList<string> QueryServices)
        {
            if (!ValidateRequest(Host, QueryServices))
                return BadRequest();

            var results = await _infoCollector.Request(Host, QueryServices.ToArray());

            return Content(JsonConvert.SerializeObject(results));
        }


        

    }
}
