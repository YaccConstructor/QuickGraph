using System;
namespace QuickGraph.Unit.Filters
{
    public sealed class AnyFixtureFilter : IFixtureFilter
    {
        public bool Filter(IFixture fixture)
        {
            return true;
        }
    }
}
