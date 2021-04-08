using Webstats.Dto.Summary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webstats.Dto.Response
{
    public class ResponseDto : Dto
    {
        public ResultSummaryDto resultSummary { get; set; }

         

        public ResponseDto()
        {
        }


    }
}
