using System;
using QuickGraph.Unit.Serialization;

namespace QuickGraph.Unit.Logging
{
    public sealed class XmlLoggerListener : ILoggerListener
    {
        private XmlLog log = new XmlLog();

        public XmlLog GetLog()
        {
            return this.log;
        }

        public void Log(LogLevel level, string message)
        {
            XmlLogEntry entry = new XmlLogEntry();
            entry.Level = level;
            entry.Message = message;

            this.log.LogEntries.Add(entry);
        }        
    }
}
