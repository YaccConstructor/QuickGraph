using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Logging
{
    [Serializable]
    internal sealed class LoggerService : ILoggerService
    {
        private volatile object syncRoot = new object();
        private List<ILoggerListener> listeners = new List<ILoggerListener>();
        public object SyncRoot
        {
            get { return this.syncRoot;}
        }

        public IList<ILoggerListener> Listeners
        {
            get 
            {
                lock(this.SyncRoot)
                {
                    return this.listeners; 
                }
            }
        }

        public void Log(LogLevel level, string message)
        {
            lock (this.SyncRoot)
            {
                foreach (ILoggerListener listener in this.listeners)
                    listener.Log(level, message);
            }
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            if (this.Listeners.Count == 0)
                return;
            string message = string.Format(format, args);
            this.Log(level, message);
        }

        public void LogMessage(string message)
        {
            this.Log(LogLevel.Message, message);
        }

        public void LogMessage(string format, params object[] args)
        {
            this.Log(LogLevel.Message, format, args);
        }

        public void LogWarning(string message)
        {
            this.Log(LogLevel.Warning, message);
        }

        public void LogWarning(string format, params object[] args)
        {
            this.Log(LogLevel.Warning, format, args);
        }

        public void LogError(string message)
        {
            this.Log(LogLevel.Error, message);
        }

        public void LogError(string format, params object[] args)
        {
            this.Log(LogLevel.Error, format, args);
        }

        public void LogError(Exception ex)
        {
            this.Log(LogLevel.Error, ex.ToString());
        }
    }
}
