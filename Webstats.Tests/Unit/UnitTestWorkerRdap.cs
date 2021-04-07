using Webstats.Tests.Unit.Interfaces;
using System;
using Xunit;

namespace Webstats.Tests
{
    public class UnitTestWorkerRdap : IUnitTestWorker
    {
        [Fact]
        public void TestIfworkerAvaibale()
        {
        }

        public void TestIfWorkerReceiveInvalidDomain()
        {
            throw new NotImplementedException();
        }

        public void TestIfWorkerReceiveValidRequest()
        {
            throw new NotImplementedException();
        }

        public void TestIfWorkerTimeout()
        {
            throw new NotImplementedException();
        }
    }
}
