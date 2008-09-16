using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Filters
{
    public sealed class AndFixtureFilter : BinaryOperationFixtureFilterBase
    {
        public AndFixtureFilter(
            IFixtureFilter left,
            IFixtureFilter right)
            : base(left, right)
        { }

        public override bool Filter(IFixture fixture)
        {
            return this.Left.Filter(fixture) && this.Right.Filter(fixture);
        }
    }
}
