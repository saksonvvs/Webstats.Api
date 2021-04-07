using System;
using System.Collections.Generic;
using System.Text;

namespace Webstats.Tests.Unit.Interfaces
{
    public interface IUnitTestWorker
    {
        void TestIfworkerAvaibale();
        void TestIfWorkerReceiveValidRequest();
        void TestIfWorkerReceiveInvalidDomain();
        void TestIfWorkerTimeout();

    }
}
