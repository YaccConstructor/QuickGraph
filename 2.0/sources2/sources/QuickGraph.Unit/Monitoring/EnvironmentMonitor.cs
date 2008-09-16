using System;
using System.Threading;
using System.Collections.Generic;

namespace QuickGraph.Unit.Monitoring
{
    internal sealed class EnvironmentMonitor : IMonitor
    {
        private string currentDirectory;

        public void Start()
        {
            this.currentDirectory = Environment.CurrentDirectory;
            
        }

        public void Stop()
        {
            if (this.currentDirectory!=null)
               Environment.CurrentDirectory = this.currentDirectory;
            this.currentDirectory = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
