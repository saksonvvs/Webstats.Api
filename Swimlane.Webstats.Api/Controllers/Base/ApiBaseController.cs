using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimlane.Webstats.Api.Controllers.Base
{
    public class ApiBaseController : Controller
    {
        private readonly ILogger<ApiBaseController> _logger;
        protected readonly IConfiguration _config;

        public ApiBaseController(
                    ILogger<ApiBaseController> logger,
                    IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

    }
}
