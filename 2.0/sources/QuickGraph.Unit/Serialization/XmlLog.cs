using System;
using System.Xml.Serialization;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlLog
    {
        private XmlLogEntryCollection logEntries = new XmlLogEntryCollection();

        [XmlArray]
        [XmlArrayItem("LogEntry")]
        public XmlLogEntryCollection LogEntries
        {
            get { return this.logEntries; }
        }
    }
}
