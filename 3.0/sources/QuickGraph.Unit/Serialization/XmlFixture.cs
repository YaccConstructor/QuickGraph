using System;
using System.Xml.Serialization;
using System.Threading;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlFixture
    {
        private string id;
        private string name;
        private string description;
        private string categories;
        private double duration=0;
        private ApartmentState apartment = ApartmentState.Unknown;
        private int timeOut = 1;
        private XmlCounter counter = new XmlCounter();
        private XmlResult fixtureSetUp = null;
        private XmlResult fixtureTearDown = null;
        private XmlTestCaseCollection testCases = new XmlTestCaseCollection();
        private int testCaseCount;

        public XmlFixture() 
        { }

        internal XmlFixture(IFixture fixture, int testCaseCount, string categories, int fixtureIndex)
        {
            this.Name = fixture.Name;
            this.testCaseCount = testCaseCount;
            this.Categories = categories;
            this.TimeOut = fixture.TimeOut;
            this.Apartment = fixture.Apartment;
            this.Description = fixture.Description;
            this.ID =
                String.Format("fix{0}", fixtureIndex);
        }

        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [XmlElement]
        public XmlCounter Counter
        {
            get { return this.counter; }
            set { this.counter = value; }
        }

        [XmlAttribute]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [XmlAttribute]
        public ApartmentState Apartment
        {
            get { return this.apartment; }
            set { this.apartment = value; }
        }

        [XmlAttribute]
        public int TimeOut
        {
            get { return this.timeOut; }
            set { this.timeOut = value; }
        }

        [XmlAttribute]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [XmlAttribute]
        public string Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        [XmlAttribute]
        public double Duration
        {
            get { return this.duration; }
            set { this.duration=value; }
        }

        [XmlElement]
        public XmlResult FixtureSetUp
        {
            get { return this.fixtureSetUp; }
            set { this.fixtureSetUp = value; }
        }

        [XmlElement]
        public XmlResult FixtureTearDown
        {
            get { return this.fixtureTearDown; }
            set { this.fixtureTearDown = value; }
        }

        [XmlArray("TestCases")]
        [XmlArrayItem("TestCase",typeof(XmlTestCase))]
        public XmlTestCaseCollection TestCases
        {
            get { return this.testCases; }
        }

        public void UpdateCounter()
        {
            this.counter = new XmlCounter();
            // the fixture setup/teardown run and failed ?            
            if ((this.FixtureSetUp != null && this.FixtureSetUp.State != TestState.Success)
                || (this.FixtureTearDown != null && this.FixtureTearDown.State != TestState.Success)
                )
            {
                this.counter.FailureCount = this.testCaseCount;
            }
            else
            {
                foreach (XmlTestCase test in this.TestCases)
                {
                    switch (test.State)
                    {
                        case TestState.Success: this.counter.SuccessCount++;
                            break;
                        case TestState.Failure: this.counter.FailureCount++;
                            break;
                        case TestState.Ignore: this.counter.IgnoreCount++;
                            break;
                        case TestState.NotRun: this.counter.NotRunCount++;
                            break;
                    }
                }
            }
            this.counter.Update();
        }
    }
}
