using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Webstats.Dto.Request
{
    public class RequestDto : AbstractDto
    {
        public string Domain { get; set; }
        public string IpAddress { get; set; }
        public IList<string> QueryServices { get; set; }


        public RequestDto()
        {
            Domain = string.Empty;
            IpAddress = string.Empty;
            QueryServices = new List<string>();
        }
    }
}
