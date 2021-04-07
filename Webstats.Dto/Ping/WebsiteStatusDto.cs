using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstats.Dto.Ping
{
    public class WebsiteStatusDto
    {
        public string Url { get; set; }
        public string Status { get; set; }
        public long Latency { get; set; }



        public WebsiteStatusDto()
        {
            Url = string.Empty;
            Status = string.Empty;
            Latency = -1;
        }



    }
}
