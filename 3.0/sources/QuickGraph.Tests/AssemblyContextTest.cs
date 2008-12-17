using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;

namespace QuickGraph.Tests
{
    [TestClass]
    public class AssemblyContextTest
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext ctx)
        {
            // avoid contract violation kill the process
            Contract.ContractFailed += new EventHandler<ContractFailedEventArgs>(Contract_ContractFailed);
        }

        static void Contract_ContractFailed(object sender, System.Diagnostics.Contracts.ContractFailedEventArgs e)
        {
            e.Handled = true;
            Assert.Fail("{0}: {1} {2}", e.FailureKind, e.DebugMessage, e.Condition);
        }
    }
}
