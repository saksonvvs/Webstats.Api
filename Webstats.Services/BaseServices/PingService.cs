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
    public class PingService : IService
    {
        private readonly string url;

        public PingService(IConfiguration config)
        {
            url = config["PingBaseUrl"];
        }


        public string ServiceType()
        {
            return ServiceTypes.Ping;
        }


        public async Task<ServiceResultDto> SendQueryAsync(string host)
        {
            if (String.IsNullOrEmpty(host))
                return new ServiceResultDto();

            ServiceResultDto result = new ServiceResultDto();
            result.ServiceType = ServiceType();
            result.Uid = Guid.NewGuid();
            



            using (HttpClient res = new HttpClient())
            {
                HttpResponseMessage response = await res.GetAsync($"{url}{host}");
                result.Result = await response.Content.ReadAsStringAsync();
                result.Status = (response.StatusCode == System.Net.HttpStatusCode.OK) ? QueryStatusTypes.Success : QueryStatusTypes.Error;

                //result.Status = (response.StatusCode == System.Net.HttpStatusCode.OK && String.IsNullOrEmpty(result.Result)) ?
            }


            return result;
        }

        
    }
}
