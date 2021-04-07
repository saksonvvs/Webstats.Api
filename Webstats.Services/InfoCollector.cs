using Microsoft.Extensions.Configuration;
using Webstats.BaseServices;
using Webstats.Common;
using Webstats.Dto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstats.Services
{
    public class InfoCollector
    {
        private readonly string[] _defaultServices;
        private readonly IEnumerable<IService> _services;


        public InfoCollector(IEnumerable<IService> services, IConfiguration config)
        {
            _services = services;

            _defaultServices = config["DefaultServices"]
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }



        public async Task<ServiceResultDto[]> Request(string host, string[] requestedServices = null)
        {
            
            var serviceTypes = requestedServices == null || requestedServices.Length == 0
                ? _defaultServices
                : requestedServices;


            var services = _services.Where(s => serviceTypes.Contains(s.ServiceType()));

            var taskResults = new List<Task<ServiceResultDto>>();
            foreach (var service in services)
            {
                var result = service.SendQueryAsync(host);
                taskResults.Add(result);
            }

            var results = await Task.WhenAll(taskResults);
            return results;
        }



    }
}
