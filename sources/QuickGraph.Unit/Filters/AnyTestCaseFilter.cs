using System;

namespace QuickGraph.Unit.Filters
{
    public sealed class AnyTestCaseFilter : ITestCaseFilter
    {
        public bool Filter(IFixture fixture, ITestCase test)
        {
            return true;
        }
    }
}
