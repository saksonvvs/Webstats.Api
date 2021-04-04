using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Webstats.Dto.Summary
{
    public class ResultSummaryDto
    {
        public string NumberOfResults { get; set; }
        public int ExecutionTime { get; set; }
        public Guid NextPageToken { get; set; }



        public ResultSummaryDto()
        {
            NumberOfResults = string.Empty;
            ExecutionTime = 0;
            NextPageToken = new Guid();
        }



    }
}
