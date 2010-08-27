using System;
using System.Diagnostics;
using QuickGraph.Unit.Exceptions;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class DebugMonitor : IMonitor
    {
        private DefaultTraceListener defaultListener;
        private DebugMonitorTraceListener listener;

        public DebugMonitor(ILoggerService logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            this.listener = new DebugMonitorTraceListener(logger);
        }

        public void Start()
        {
            // find default listnerer
            foreach (TraceListener tc in Debug.Listeners)
            {
                this.defaultListener = tc as DefaultTraceListener;
                if (defaultListener != null)
                    break;
            }

            // remove default listener
            if (this.defaultListener != null)
                Debug.Listeners.Remove(this.defaultListener);

            // adding custom
            Debug.Listeners.Add(this.listener);
        }

        public void Stop()
        {
            if (this.defaultListener != null)
            {
                Debug.Listeners.Add(this.defaultListener);
                this.defaultListener = null;
            }
            Debug.Listeners.Remove(this.listener);
        }

        public void Dispose()
        {
            this.Stop();
        }

        private sealed class DebugMonitorTraceListener : TraceListener
        {
            private ILoggerService logger;
            private DefaultTraceListener defaultListener;

            public DebugMonitorTraceListener(ILoggerService logger)
            {
                this.logger = logger;
                foreach (TraceListener tc in Debug.Listeners)
                {
                    this.defaultListener = tc as DefaultTraceListener;
                    if (defaultListener != null)
                        break;
                }
            }

            private void WriteLine(string format, params object[] args)
            {
                this.logger.LogMessage("Debug", format, args);
                if (this.defaultListener != null)
                    this.defaultListener.WriteLine(String.Format(format, args));
            }

            public override void Fail(string message)
            {
                this.logger.LogError("Debug", message);
                throw new DebugFailureException(message);
            }

            public override void Fail(string message, string messageDetail)
            {
                this.logger.LogError("Debug", "{0}\n{1}", message, messageDetail);
                throw new DebugFailureException(message + messageDetail);
            }

            public override void Write(string message)
            {
                this.logger.LogMessage("Debug", message);
                if (this.defaultListener != null)
                    this.defaultListener.Write(message);
            }

            public override void WriteLine(string message)
            {
                this.logger.LogMessage("Debug", message);
                if (this.defaultListener != null)
                    this.defaultListener.WriteLine(message);
            }

            public override void Write(string message, string category)
            {
                this.logger.LogMessage(category, message);
                if (this.defaultListener != null)
                    this.defaultListener.Write(message,category);
            }

            public override void WriteLine(string message, string category)
            {
                this.logger.LogMessage(category, message);
                if (this.defaultListener != null)
                    this.defaultListener.WriteLine(message, category);
            }
        }
    }
}
