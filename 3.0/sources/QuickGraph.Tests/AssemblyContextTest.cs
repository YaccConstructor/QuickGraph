using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;
using System.Diagnostics;

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
            string message = string.Format("{0}, {1}", e.DebugMessage, e.Condition);
            e.Handled = true;
            switch (e.FailureKind)
            {
                case ContractFailureKind.Precondition:
                    Debug.Assert(false, message);
                    break;
                default:
                    Assert.Fail(message);
                    break;
            }
        }
    }
}
