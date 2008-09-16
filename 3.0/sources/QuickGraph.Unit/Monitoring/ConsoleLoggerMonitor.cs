using System;
using QuickGraph.Unit.Logging;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class ConsoleLoggerMonitor : IMonitor
    {
        private ILoggerService logger;
        private ConsoleLoggerListener listener = new ConsoleLoggerListener();

        public ConsoleLoggerMonitor(ILoggerService logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            this.logger = logger;
        }

        public void  Start()
        {
            this.logger.Listeners.Add(this.listener);
        }

        public void  Stop()
        {
            this.logger.Listeners.Remove(this.listener);
        }

        public void  Dispose()
        {
            this.Stop();
        }
    }
}
