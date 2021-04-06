﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swimlane.Webstats.Workers.PingHost.Dto
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
