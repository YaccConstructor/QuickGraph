using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    public abstract class FixtureBase  : IFixture
    {
        private ApartmentState apartment;
        private int timeOut;
        private string description = null;
        private bool isCurrent = false;
        private List<ITestCaseDecorator> testCaseDecorators = new List<ITestCaseDecorator>();
        private List<string> categories = new List<string>();
        private MethodInfo fixtureSetup;
        private MethodInfo fixtureTearDown;
        private MethodInfo setUp;
        private MethodInfo tearDown;

        public FixtureBase(ApartmentState apartment, int timeOut, string description)
        {
            this.apartment = apartment;
            this.timeOut = timeOut;
            this.description = description;
        }

        public ApartmentState Apartment
        {
            get { return this.apartment; }
        }

        public int TimeOut
        {
            get { return this.timeOut; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public bool IsCurrent
        {
            get { return this.isCurrent; }
            set { this.isCurrent = value; }
        }

        public IList<string> Categories
        {
            get { return this.categories; }
        }

        public IList<ITestCaseDecorator> TestCaseDecorators
        {
            get { return this.testCaseDecorators; }
        }

        public MethodInfo FixtureSetUp 
        {
            get { return this.fixtureSetup; }
            set { this.fixtureSetup = value; }
        }
        public MethodInfo SetUp
        {
            get { return this.setUp; }
            set { this.setUp = value; }
        }
        public MethodInfo TearDown
        {
            get { return this.tearDown; }
            set { this.tearDown = value; }
        }
        public MethodInfo FixtureTearDown
        {
            get { return this.fixtureTearDown; }
            set { this.fixtureTearDown = value; }
        }

        public abstract string Name {get;}
        public abstract Object CreateInstance();
        public abstract IEnumerable<ITestCase> CreateTestCases();

        public override string ToString()
        {
            return this.Name;
        }
    }
}
