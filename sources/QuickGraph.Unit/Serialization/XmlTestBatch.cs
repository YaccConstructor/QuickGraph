using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;

namespace QuickGraph.Unit.Serialization
{
	[XmlRoot("TestBatch")]
    [Serializable]
    public sealed class XmlTestBatch
    {
        private XmlMachine machine;
        private string entryAssemblyName = null;
        private string entryAssemblyLocation = null;
        private XmlCounter counter = new XmlCounter();
        private XmlTestAssemblyCollection testAssemblies = new XmlTestAssemblyCollection();
        private string commandLine = null;
        private string startTime = DateTime.Now.ToString("u");
        private string endTime = DateTime.Now.ToString("u");
        private double duration;
        private bool hasHistory = false;
        private XmlLog log = new XmlLog();

        public XmlTestBatch()
        { }

        public void SetMainAssembly(Assembly entryAssembly)
        {
            this.machine = XmlMachine.Create();
            this.commandLine = Environment.CommandLine;
            if (entryAssembly != null)
            {
                this.entryAssemblyLocation = Path.GetFileName(entryAssembly.Location);
                this.entryAssemblyName = entryAssembly.GetName().Name;
            }
        }

        [XmlAttribute]
        public bool HasHistory
        {
            get { return this.hasHistory; }
            set { this.hasHistory = value; }
        }

        [XmlAttribute]
        public string CommandLine
        {
            get { return this.commandLine; }
            set { this.commandLine = value; }
        }

        [XmlAttribute]
        public string EntryAssemblyLocation
        {
            get { return this.entryAssemblyLocation; }
            set { this.entryAssemblyLocation = value; }
        }

        [XmlAttribute]
        public string EntryAssemblyName
        {
            get { return this.entryAssemblyName; }
            set { this.entryAssemblyName = value; }
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
        public XmlCounter Counter
        {
            get { return this.counter; }
            set { this.counter = value; }
        }

        [XmlElement]
        public XmlMachine Machine
        {
            get { return this.machine; }
            set { this.machine = value; }
        }

        [XmlElement]
        public XmlLog Log
        {
            get { return this.log; }
            set { this.log = value; }
        }

        [XmlArray("TestAssemblies")]
        [XmlArrayItem("TestAssembly")]
        public XmlTestAssemblyCollection TestAssemblies
        {
            get { return this.testAssemblies; }
        }

        public void UpdateCounter()
        {
            this.counter = new XmlCounter();
            this.duration = 0;

            foreach (XmlTestAssembly testAssembly in this.TestAssemblies)
            {
                testAssembly.UpdateCounter();
                this.counter = this.counter + testAssembly.Counter;
                this.duration += testAssembly.Duration;
            }
        }
    }
}
