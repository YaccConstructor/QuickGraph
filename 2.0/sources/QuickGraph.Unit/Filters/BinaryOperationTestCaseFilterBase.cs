using System;

namespace QuickGraph.Unit.Filters
{
    public abstract class BinaryOperationTestCaseBase : ITestCaseFilter
    {
        private ITestCaseFilter left;
        private ITestCaseFilter right;

        public BinaryOperationTestCaseBase(
            ITestCaseFilter left,
            ITestCaseFilter right)
        {
            this.left = left;
            this.right = right;
        }

        public ITestCaseFilter Left
        {
            get { return this.left; }
        }

        public ITestCaseFilter Right
        {
            get { return this.right; }
        }

        public abstract bool Filter(IFixture fixture, ITestCase testCase);
    }
}
