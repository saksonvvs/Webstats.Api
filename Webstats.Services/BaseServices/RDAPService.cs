using Microsoft.Extensions.Configuration;
using Webstats.BaseServices;
using Webstats.Common;
using Webstats.Dto.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Webstats.Services.BaseServices
{
    public class RDAPService : IService
    {
        private readonly string url;

        public RDAPService(IConfiguration config)
        {
            url = config["RDAPBaseUrl"];
        }

        public string ServiceType()
        {
            return ServiceTypes.RDAP;
        }


        public async Task<ServiceResultDto> SendQueryAsync(string host)
        {
            if (String.IsNullOrEmpty(host))
                return new ServiceResultDto() { ServiceType = ServiceType(), Status = QueryStatusTypes.Error };


            ServiceResultDto result = new ServiceResultDto();
            result.ServiceType = ServiceType();
            result.Uid = Guid.NewGuid();

            try
            {

                using (HttpClient res = new HttpClient())
                {
                    res.Timeout = new TimeSpan(0, 0, 5);
                    HttpResponseMessage response = await res.GetAsync($"{url}{host}");
                    result.Result = await response.Content.ReadAsStringAsync();
                    result.Status = (response.StatusCode == System.Net.HttpStatusCode.OK) ? QueryStatusTypes.Success : QueryStatusTypes.Error;
                }

            }
            catch (Exception ex)
            {
                return new ServiceResultDto() { ServiceType = ServiceType(), Status = QueryStatusTypes.Error };
            }

            return result;
        }


    }
}
