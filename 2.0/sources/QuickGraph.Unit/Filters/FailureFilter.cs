using System;
using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Filters
{
    public sealed class FailureFilter : IFixtureFilter, ITestCaseFilter
    {
        private XmlTestBatchSearcher searcher;

        public FailureFilter(XmlTestBatch testBatch)
        {
            if (testBatch == null)
                throw new ArgumentNullException("testBatch");
            this.searcher = new XmlTestBatchSearcher(testBatch);
        }

        public XmlTestBatchSearcher Searcher
        {
            get { return this.searcher; }
        }

        public bool Filter(IFixture fixture)
        {
            XmlFixture xfixture = this.Searcher.GetFixture(fixture.Name);

            if (xfixture == null)
                return false;
            else
                return xfixture.Counter.FailureCount > 0;
        }

        public bool Filter(IFixture fixture, ITestCase testCase)
        {
            XmlTestCase xtestCase = this.Searcher.GetTestCase(fixture.Name, testCase.Name);
            if (xtestCase == null)
                return false;
            else
                return xtestCase.State == TestState.Failure;
        }
    }
}
