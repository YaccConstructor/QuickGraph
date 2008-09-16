using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Filters
{
    public sealed class CurrentFixtureFilter : IFixtureFilter
    {
        public bool Filter(IFixture fixture)
        {
            return fixture.IsCurrent;
        }
    }
}
