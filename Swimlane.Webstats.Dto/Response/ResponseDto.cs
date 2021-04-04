using Swimlane.Webstats.Dto.Summary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Webstats.Dto.Response
{
    public class ResponseDto : AbstractDto
    {
        public ResultSummaryDto resultSummary { get; set; }

         

        public ResponseDto()
        {
        }


    }
}
