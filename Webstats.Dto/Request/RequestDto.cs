using System;
using System.Collections.Generic;
using System.Text;

namespace Webstats.Dto.Request
{
    public class RequestDto : AbstractRequestDto
    {

        public RequestDto(string HostName, IEnumerable<string> Services)
        {
            Host = HostName;
            QueryServices = Services;
        }

    }
}
