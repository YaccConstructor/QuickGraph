using System;

namespace QuickGraph.Unit.Filters
{
    public sealed class AndTestCaseFilter : BinaryOperationTestCaseBase
    {
        public AndTestCaseFilter(ITestCaseFilter left, ITestCaseFilter right)
            : base(left, right)
        { }

        public override bool Filter(IFixture fixture, ITestCase testCase)
        {
            return Left.Filter(fixture, testCase) && Right.Filter(fixture, testCase);
        }
    }
}
