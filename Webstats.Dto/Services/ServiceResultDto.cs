using Webstats.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webstats.Dto.Services
{
    public class ServiceResultDto : Dto
    {
        public string ServiceType { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }



        public ServiceResultDto()
        {
            ServiceType = ServiceTypes.Unknown;
            Result = string.Empty;
            Status = string.Empty;
        }
    }
}
