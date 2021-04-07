using Webstats.Dto.Services;
using System;
using System.Threading.Tasks;

namespace Webstats.BaseServices
{
    public interface IService
    {
        string ServiceType();

        Task<ServiceResultDto> SendQueryAsync(string host);
    }
}
