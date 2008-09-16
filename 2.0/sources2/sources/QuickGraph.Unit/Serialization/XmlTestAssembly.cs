using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlTestAssembly
    {
        private XmlCounter counter = new XmlCounter();
        private XmlFixtureCollection fixtures = new XmlFixtureCollection();
        private XmlResult assemblySetUp = null;
        private XmlResult assemblyTearDown = null;
        private string assemblyName = null;
        private string assemblyFullName = null;
        private string assemblyLocation = null;
        private DateTime startTime;
        private DateTime endTime;
        private double duration;

        public XmlTestAssembly()
        {}

        public XmlTestAssembly(AssemblyName assemblyName, string location)
            : this()
        {
            this.assemblyName = assemblyName.Name;
            this.assemblyFullName = assemblyName.FullName;
            this.assemblyLocation = location;
        }

        [XmlAttribute]
        public string AssemblyName
        {
            get { return this.assemblyName; }
            set { this.assemblyName = value; }
        }

        [XmlAttribute]
        public string AssemblyFullName
        {
            get { return this.assemblyFullName; }
            set { this.assemblyFullName = value; }
        }

        [XmlAttribute]
        public string AssemblyLocation
        {
            get { return this.assemblyLocation; }
            set { this.assemblyLocation = value; }
        }

        [XmlAttribute]
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        [XmlAttribute]
        public DateTime EndTime
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
        public XmlResult AssemblySetUp
        {
            get { return this.assemblySetUp; }
            set { this.assemblySetUp = value; }
        }

        [XmlElement]
        public XmlResult AssemblyTearDown
        {
            get { return this.assemblyTearDown; }
            set { this.assemblyTearDown = value; }
        }

        [XmlArray("Fixtures")]
        [XmlArrayItem("Fixture")]
        public XmlFixtureCollection Fixtures
        {
            get { return this.fixtures; }
        }

        public void UpdateCounter()
        {
            this.counter = new XmlCounter();
            this.duration = 0;
            foreach (XmlFixture fixture in this.Fixtures)
            {
                fixture.UpdateCounter();
                this.counter = this.counter + fixture.Counter;
                this.duration += fixture.Duration;
            }
        }
    }
}
