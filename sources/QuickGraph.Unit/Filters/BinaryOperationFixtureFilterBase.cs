using System;

namespace QuickGraph.Unit.Filters
{
    public abstract class BinaryOperationFixtureFilterBase : IFixtureFilter
    {
        private IFixtureFilter left;
        private IFixtureFilter right;

        public BinaryOperationFixtureFilterBase(
            IFixtureFilter left,
            IFixtureFilter right)
        {
            this.left = left;
            this.right = right;
        }

        public IFixtureFilter Left
        {
            get { return this.left; }
        }

        public IFixtureFilter Right
        {
            get { return this.right; }
        }

        public abstract bool Filter(IFixture fixture);
    }
}
