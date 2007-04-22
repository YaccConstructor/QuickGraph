using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class MonitorCollection : List<IMonitor>, IDisposable
    {
        private ILoggerService logger;
        public MonitorCollection(ILoggerService logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            this.logger = logger;
        }

        public void Start()
        {
            foreach (IMonitor monitor in this)
            {
                monitor.Start();
            }
        }

        public void Stop()
        {
            foreach (IMonitor monitor in this)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    monitor.Stop();
                }
                catch (Exception ex)
                {
                    this.logger.LogError("Monitoring", ex, "monitor {0} failed to stop", monitor);
                }
            }
        }

        public void Dispose()
        {
            this.Stop();
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                foreach (IMonitor monitor in this)
                    monitor.Dispose();
            }
            finally
            {
                this.Clear();
            }
        }
    }
}
