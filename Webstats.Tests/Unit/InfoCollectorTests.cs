using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstats.BaseServices;
using Webstats.Common;
using Webstats.Dto.Services;
using Webstats.Services;
using Xunit;

namespace Webstats.Tests.Unit
{
    public class InfoCollectorTests
    {
        [Fact]
        public void ShouldUseDefaultServices()
        {
            var rdapService = new Mock<IService>();
            rdapService
                .Setup(e => e.ServiceType())
                .Returns(ServiceTypes.RDAP);

            rdapService
                .Setup(e => e.SendQueryAsync("google.com"))
                .Returns(Task.FromResult(new ServiceResultDto
                {
                    Result = "DATA FROM RDAP"
                }));


            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(c => c.Value)
                .Returns("RDAP,Ping");

            var config = new Mock<IConfiguration>();    

            config.Setup(s => s.GetSection("DefaultServices"))
                .Returns(configSection.Object);


            var infoCollector = new InfoCollector(new[] { rdapService.Object }, config.Object);

            var results = infoCollector.Request("google.com").Result;

            Assert.Single(results);
            Assert.Equal("DATA FROM RDAP", results[0].Result);
        }


        [Fact]
        public async void ShouldReturnEmptyResults()
        {
            string HostName = "google.com";

            var services = new List<IService>();

            var rdapService = new Mock<IService>();
            rdapService
                .Setup(e => e.ServiceType())
                .Returns(ServiceTypes.RDAP);

            rdapService
                .Setup(e => e.SendQueryAsync(HostName))
                .Returns(Task.FromResult(new ServiceResultDto()
                {
                    Result = "",
                    Status = "Error"
                }));

            services.Add(rdapService.Object);


            var pingService = new Mock<IService>();
            pingService
                .Setup(e => e.ServiceType())
                .Returns(ServiceTypes.Ping);

            pingService
                .Setup(e => e.SendQueryAsync(HostName))
                .Returns(Task.FromResult(new ServiceResultDto()
                {
                    Result = "",
                    Status = "Error"
                }));

            services.Add(pingService.Object);


           

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(c => c.Value)
                .Returns("RDAP,Ping");

            var config = new Mock<IConfiguration>();

            config.Setup(s => s.GetSection("DefaultServices"))
                .Returns(configSection.Object);


            var infoCollector = new InfoCollector(services, config.Object);


            var results = infoCollector.Request("google.com").Result;


            foreach(var item in results)
            {
                Assert.Equal("Error", item.Status);
            }
        }

    }
}
