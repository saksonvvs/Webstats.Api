using System;
using System.Collections.Generic;
using System.Text;

namespace Webstats.Dto.Ping
{
    public class PingReplyDto
    {
        public string Address { get; set; }
        public long RoundtripTime { get; set; }
        public int Ttl { get; set; }
        public bool DontFragment { get; set; }
        public int BufferSize { get; set; }


        public PingReplyDto()
        {
            Address = string.Empty;
            RoundtripTime = -1;
            Ttl = -1;
            DontFragment = false;
            BufferSize = -1;
        }
    }
}
