using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Swimlane.Webstats.Dto.Request
{
    public abstract class AbstractRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Host { get; set; }

        public IList<string> QueryServices { get; set; }

        public AbstractRequestDto()
        {
            Host = string.Empty;
            QueryServices = new List<string>();
        }


    }
}
