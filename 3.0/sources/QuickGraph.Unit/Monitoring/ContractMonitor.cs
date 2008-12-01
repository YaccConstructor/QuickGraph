using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using __Substitutions.System.Diagnostics;

namespace QuickGraph.Unit.Monitoring
{
    class ContractMonitor : IMonitor
    {
        public void Start()
        {
            Contract.ContractFailed += new EventHandler<ContractFailedEventArgs>(Contract_ContractFailed);
        }

        public void Stop()
        {
            Contract.ContractFailed -= new EventHandler<ContractFailedEventArgs>(Contract_ContractFailed);
        }

        void Contract_ContractFailed(object sender, ContractFailedEventArgs e)
        {
            e.Handled = true;
            Debugger.Break();
            Assert.Fail("{0}: {1} {2}", e.FailureKind, e.Condition, e.DebugMessage);
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}
