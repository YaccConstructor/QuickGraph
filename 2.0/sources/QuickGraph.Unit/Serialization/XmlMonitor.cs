using System;
using System.IO;
using System.Xml.Serialization;
using QuickGraph.Unit.Monitoring;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public class XmlMonitor
    {
        private string consoleOut;
        private string consoleError;
        private string startTime;
        private string endTime;
        private double duration;
        private XmlLog log = new XmlLog();

        public XmlMonitor()
        {}

        public XmlMonitor(TestMonitor monitor)
        {
            this.Update(monitor);
        }

        public void Update(TestMonitor monitor)
        {
            this.consoleError = UnitSerializer.XmlSerializerEscapeWorkAround(monitor.Console.Error);
            this.consoleOut = UnitSerializer.XmlSerializerEscapeWorkAround(monitor.Console.Out);
            this.startTime = monitor.Timer.StartTime.ToString("u");
            this.endTime = monitor.Timer.EndTime.ToString("u");
            this.duration = monitor.Timer.Duration;
            this.log = monitor.GetLog();
        }

        [XmlAttribute]
        public string StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        [XmlAttribute]
        public string EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        [XmlAttribute]
        public double Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [XmlElement]
        public XmlLog Log
        {
            get { return this.log; }
            set { this.log = value; }
        }

        [XmlElement]
        public string ConsoleOut
        {
            get { return this.consoleOut; }
            set { this.consoleOut = value; }
        }

        [XmlElement]
        public string ConsoleError
        {
            get { return this.consoleError; }
            set { this.consoleError = value; }
        }
    }
}
