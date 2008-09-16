using System;
using System.IO;
using System.Xml.Serialization;
using QuickGraph.Unit.Monitoring;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlTestCase
    {
        private string id;
        private TestState state = TestState.NotRun;
        private string name;
        private double duration=0;
        private XmlResult setUp = null;
        private XmlResult tearDown = null;
        private XmlResult test = null;
        private XmlTestHistory history = XmlTestHistory.NoChange;

        public XmlTestCase()
        { }

        public XmlTestCase(string id,string name)
        {
            this.id = id;
            this.name = name;
        }

        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [XmlAttribute]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public static string GetTestCaseFullName(string fixtureName, string testName)
        {
            return string.Format("{0}.{1}", fixtureName, testName);
        }
        public string GetFullName(XmlFixture fixture)
        {
            return GetTestCaseFullName(fixture.Name, this.Name);
        }

        [XmlAttribute]
        public double Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [XmlAttribute]
        public XmlTestHistory History
        {
            get { return this.history; }
            set 
            { 
                this.history = value;
            }
        }

        [XmlElement]
        public XmlResult SetUp
        {
            get { return this.setUp; }
            set 
            { 
                this.setUp = value;
                this.Update();
            }
        }

        [XmlElement]
        public XmlResult TearDown
        {
            get { return this.tearDown; }
            set 
            { 
                this.tearDown=value;
                this.Update();
            }
        }

        [XmlElement]
        public XmlResult Test
        {
            get { return this.test; }
            set 
            { 
                this.test = value;
                this.Update();
            }
        }

        [XmlAttribute]
        public TestState State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public void Update()
        {
            this.duration = 0;
            if (this.SetUp != null)
            {
                this.state = MaxState(this.state, this.SetUp.State);
                this.duration += this.SetUp.Duration;
            }

            if (this.Test != null)
            {
                this.state = MaxState(this.state, this.Test.State);
                this.duration += this.Test.Duration;
            }

            if (this.TearDown != null)
            {
                this.state = MaxState(this.state, this.TearDown.State);
                this.duration += this.TearDown.Duration;
            }
        }

        private static TestState MaxState(TestState left, TestState right)
        {
            if ((short)left > (short)right)
                return left;
            else
                return right;
        }
    }
}
