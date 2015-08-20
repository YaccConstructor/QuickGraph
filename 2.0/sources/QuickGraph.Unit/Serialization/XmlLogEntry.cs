using System;
using System.Xml.Serialization;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlLogEntry
    {
        private LogLevel level = LogLevel.Message;
        private DateTime time;
        private string message;
        private XmlException exception;

        public XmlLogEntry()
        {
            this.time = DateTime.Now;
        }

        public XmlLogEntry(
            LogLevel level,
            string message
            )
        {
            this.level = level;
            this.message = message;
        }

        public XmlLogEntry(Exception ex)
        {
            this.level = LogLevel.Error;
            this.message = ex.Message;
            this.exception = XmlException.FromException(ex);
        }

        [XmlAttribute]
        public LogLevel Level
        {
            get { return this.level; }
            set { this.level = value; }
        }

        [XmlAttribute]
        public DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        [XmlAttribute]
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        [XmlElement]
        public XmlException Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }
    }
}
