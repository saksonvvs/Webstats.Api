using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webstats.Dto.Request
{
    public abstract class AbstractRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Host { get; set; }

        public IEnumerable<string> QueryServices { get; set; }

        public AbstractRequestDto()
        {
            Host = string.Empty;
            QueryServices = new List<string>();
        }


    }
}
