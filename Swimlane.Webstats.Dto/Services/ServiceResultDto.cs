using Swimlane.Webstats.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Webstats.Dto.Services
{
    public class ServiceResultDto : AbstractDto
    {
        public string ServiceType { get; set; }
        public string Result { get; set; }


        public ServiceResultDto()
        {
            ServiceType = ServiceTypes.Unknown;
            Result = string.Empty;
        }
    }
}
