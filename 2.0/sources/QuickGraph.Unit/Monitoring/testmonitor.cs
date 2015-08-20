using System;
using System.Collections.Generic;
using QuickGraph.Unit.Logging;
using QuickGraph.Unit.Serialization;

namespace QuickGraph.Unit.Monitoring
{
    public class TestMonitor : IMonitor
    {
        private ILoggerService loggerService;
        private MonitorCollection monitors;
        private ConsoleMonitor consoleMonitor;
        private TimeMonitor timeMonitor;
        private UnhandledExceptionMonitor unhandledExceptionMonitor;
        private ThreadExceptionMonitor threadExceptionMonitor;
        private DebugMonitor debugMonitor;
        private XmlLoggerListener loggerListener = null;

        public TestMonitor()
            :this(Assert.Logger)
        { }

        public TestMonitor(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
            this.consoleMonitor = new ConsoleMonitor();
            this.timeMonitor = new TimeMonitor();
            this.unhandledExceptionMonitor = new UnhandledExceptionMonitor();
            this.threadExceptionMonitor = new ThreadExceptionMonitor(loggerService);
            this.debugMonitor = new DebugMonitor(loggerService);
            this.loggerListener = new XmlLoggerListener();

            this.monitors = new MonitorCollection(loggerService);
            this.monitors.Add(this.consoleMonitor);
            this.monitors.Add(this.timeMonitor);
            this.monitors.Add(this.unhandledExceptionMonitor);
            this.monitors.Add(this.threadExceptionMonitor);
            this.monitors.Add(this.debugMonitor);
            this.monitors.Add(new EnvironmentMonitor());
            this.monitors.Add(new ThreadMonitor());
        }

        public ILoggerService LoggerService
        {
            get { return this.loggerService; }
        }

        public MonitorCollection Monitors
        {
            get { return this.monitors; }
        }

        public ConsoleMonitor Console
        {
            get { return this.consoleMonitor; }
        }

        public TimeMonitor Timer
        {
            get { return this.timeMonitor; }
        }

        public UnhandledExceptionMonitor UnhandledException
        {
            get { return this.unhandledExceptionMonitor; }
        }

        public ThreadExceptionMonitor ThreadException
        {
            get { return this.threadExceptionMonitor; }
        }

        public DebugMonitor Debug
        {
            get { return this.debugMonitor; }
        }

        public XmlLog GetLog()
        {
            return this.loggerListener.GetLog();
        }

        public void Start()
        {
            this.loggerListener = new XmlLoggerListener();
            this.LoggerService.Listeners.Add(this.loggerListener);
            this.monitors.Start();
        }

        public void Stop()
        {
            this.monitors.Stop();
            this.LoggerService.Listeners.Remove(this.loggerListener);            
        }

        public void Dispose()
        {
            this.Stop();
            if (this.monitors != null)
            {
                this.monitors.Dispose();
                this.monitors = null;
            }
        }
    }
}
