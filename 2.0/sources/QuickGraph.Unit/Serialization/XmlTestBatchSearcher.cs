using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Serialization
{
    public sealed class XmlTestBatchSearcher
    {
        private XmlTestBatch testBatch;
        private Dictionary<string, XmlTestCase> testCases = new Dictionary<string, XmlTestCase>();
        private Dictionary<string, XmlFixture> fixtures = new Dictionary<string, XmlFixture>();

        public XmlTestBatchSearcher(XmlTestBatch testBatch)
        {
            this.testBatch = testBatch;

            foreach (XmlTestAssembly testAssembly in this.testBatch.TestAssemblies)
            {
                foreach (XmlFixture fixture in testAssembly.Fixtures)
                {
                    this.fixtures[fixture.Name]=fixture;
                    foreach (XmlTestCase testCase in fixture.TestCases)
                    {
                        string testName = testCase.GetFullName(fixture);
                        if (this.testCases.ContainsKey(testName))
                            Console.WriteLine("Warning: duplicate test {0}", testName);
                        else
                            this.testCases.Add(testName,testCase);
                    }
                }
            }
        }

        public IDictionary<string, XmlTestCase> TestCases
        {
            get { return this.testCases; }
        }

        public IDictionary<string, XmlFixture> Fixtures
        {
            get { return this.fixtures; }
        }

        public XmlFixture GetFixture(XmlFixture fixture)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");
            return GetFixture(fixture.Name);
        }

        public XmlFixture GetFixture(string fixtureName)
        {
            XmlFixture tc = null;
            this.fixtures.TryGetValue(fixtureName, out tc);
            return tc;
        }

        public XmlTestCase GetTestCase(string fixtureName, string testCaseName)
        {
            XmlTestCase tc = null;
            this.testCases.TryGetValue(
                XmlTestCase.GetTestCaseFullName(fixtureName,testCaseName),
                out tc);
            return tc;
        }

        public XmlTestCase GetTestCase(XmlFixture fixture, XmlTestCase testCase)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");
            if (testCase == null)
                throw new ArgumentNullException("testCase");

            return GetTestCase(fixture.Name, testCase.Name);
        }
    }
}
