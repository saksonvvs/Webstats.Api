using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Webstats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webstats.Api.Controllers.Base
{
    public class ApiBaseController : Controller
    {
        private readonly ILogger<ApiBaseController> _logger;
        protected readonly IConfiguration _config;

        public ApiBaseController(
                    ILogger<ApiBaseController> logger,
                    IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }




        protected bool ValidateRequest(string Host, IList<string> QueryServices)
        {
            if (String.IsNullOrEmpty(Host))
                return false;

            //validate domain or ip
            bool isValidIp = false;
            bool isValidDomain = false;

            isValidIp = ValidateIPv4(Host);
            isValidDomain = ValidateDomainName(Host);

            if (!isValidIp && !isValidDomain)
                return false;

            if (QueryServices != null && QueryServices.Count > 0)
            {
                var allServices = ServiceTypes.ServiceList.Select(s => s.ToLower()).ToArray();
                foreach (var service in QueryServices)
                {
                    if (!allServices.Contains(service.ToLower()))
                        return false;
                }
            }


            return true;
        }

        private bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
                return false;

            string[] splitValues = ipString.Split('.');

            if (splitValues.Length != 4)
                return false;

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        private bool ValidateDomainName(string name)
        {
            //UriHostNameType hostType = Uri.CheckHostName(name);
            //return hostType != UriHostNameType.Unknown;

            if (Regex.IsMatch(name, @" # Rev:2013-03-26
                    # Match DNS host domain having one or more subdomains.
                    # Top level domain subset taken from IANA.ORG. See:
                    # http://data.iana.org/TLD/tlds-alpha-by-domain.txt
                    ^                  # Anchor to start of string.
                    (?!.{256})         # Whole domain must be 255 or less.
                    (?:                # Group for one or more sub-domains.
                      [a-z0-9]         # Either subdomain length from 2-63.
                      [a-z0-9-]{0,61}  # Middle part may have dashes.
                      [a-z0-9]         # Starts and ends with alphanum.
                      \.               # Dot separates subdomains.
                    | [a-z0-9]         # or subdomain length == 1 char.
                      \.               # Dot separates subdomains.
                    )+                 # One or more sub-domains.
                    (?:                # Top level domain alternatives.
                      [a-z]{2}         # Either any 2 char country code,
                    | AERO|ARPA|ASIA|BIZ|CAT|COM|COOP|EDU|  # or TLD 
                      GOV|INFO|INT|JOBS|MIL|MOBI|MUSEUM|    # from list.
                      NAME|NET|ORG|POST|PRO|TEL|TRAVEL|XXX  # IANA.ORG
                    )                  # End group of TLD alternatives.
                    $                  # Anchor to end of string.",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
        }



    }
}
