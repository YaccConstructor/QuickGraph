using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Filters;

namespace QuickGraph.Unit.Core
{
    public sealed class TestBatch
    {
        private List<TestAssembly> testAssemblies = new List<TestAssembly>();

        public TestBatch()
        {}

        public ICollection<TestAssembly> TestAssemblies
        {
            get { return this.testAssemblies; }
        }

        public TestAssembly MainTestAssembly
        {
            get
            {
                if (this.TestAssemblies.Count > 0)
                    return this.testAssemblies[0];
                else
                    return null;
            }
        }

        public int GetFixtureCount()
        {
            int count = 0;
            foreach (TestAssembly testAssembly in this.TestAssemblies)
                count += testAssembly.Fixtures.Count;
            return count;
        }

        public int GetTestCount()
        {
            int count = 0;
            foreach (TestAssembly testAssembly in this.TestAssemblies)
                count+=testAssembly.GetTestCount();
            return count;
        }
    }
}
