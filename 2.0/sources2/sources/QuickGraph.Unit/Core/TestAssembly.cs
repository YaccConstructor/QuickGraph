using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Filters;

namespace QuickGraph.Unit.Core
{
    public sealed class TestAssembly
    {
        private Assembly assembly;
        private MethodInfo assemblySetUp;
        private MethodInfo assemblyTearDown;
        private Dictionary<IFixture, ICollection<ITestCase>> fixtureTestCases = new Dictionary<IFixture, ICollection<ITestCase>>();

        public TestAssembly(
            Assembly assembly
            )
        {
            this.assembly = assembly;
        }

        public Assembly Assembly
        {
            get { return this.assembly; }
        }

        public MethodInfo AssemblySetUp
        {
            get { return this.assemblySetUp; }
            set { this.assemblySetUp = value; }
        }

        public MethodInfo AssemblyTearDown
        {
            get { return this.assemblyTearDown; }
            set { this.assemblyTearDown = value; }
        }

        public ICollection<IFixture> Fixtures
        {
            get { return this.fixtureTestCases.Keys; }
        }

        public ICollection<ITestCase> GetTestCasesFromFixture(IFixture fixture)
        {
            return this.fixtureTestCases[fixture];
        }

        public void AddFixture(IFixture fixture, ICollection<ITestCase> testCases)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");
            if (testCases == null)
                throw new ArgumentNullException("testCases");
            this.fixtureTestCases.Add(fixture, testCases);
        }

        public int GetTestCount()
        {
            int count = 0;
            foreach (ICollection<ITestCase> tests in this.fixtureTestCases.Values)
                count += tests.Count;
            return count;
        }
    }
}
