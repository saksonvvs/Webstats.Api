using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webstats.Dto.Request
{
    public class RequestDto : Dto
    {
        [Required]
        [MaxLength(50)]
        public string Host { get; set; }

        public IEnumerable<string> QueryServices { get; set; }

        public RequestDto()
        {
            Host = string.Empty;
            QueryServices = new List<string>();
        }

        public RequestDto(string HostName, IEnumerable<string> Services)
        {
            Host = HostName;
            QueryServices = Services;
        }

    }
}
