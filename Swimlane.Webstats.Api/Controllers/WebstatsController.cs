using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swimlane.Webstats.Api.Controllers.Base;
using Swimlane.Webstats.BaseServices;
using Swimlane.Webstats.Common;
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
    public class WebstatsController : ApiBaseController
    {
       
        private readonly IEnumerable<IService> _services;


        public WebstatsController(
            ILogger<WebstatsController> logger,
            IConfiguration config,
            IEnumerable<IService> services) :base(logger, config)
        {
            _services = services;
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(RequestDto request)
        {
            if (request == null)
                return BadRequest();

            TryValidateModel(request);

            if (!ModelState.IsValid)
                return BadRequest();


            IService RDAPService = _services.Where(x => x.ServiceType() == ServiceTypes.RDAP).Single();
            ServiceResultDto rdapResult = await RDAPService.SendQueryAsync(request.Host);
            

            IService PingService = _services.Where(x => x.ServiceType() == ServiceTypes.Ping).Single();
            ServiceResultDto pingResult = await PingService.SendQueryAsync(request.Host);


            IList<ServiceResultDto> result = new List<ServiceResultDto>();
            result.Add(rdapResult);
            result.Add(pingResult);
            

            return Content(JsonConvert.SerializeObject(result));
        }




    }
}
