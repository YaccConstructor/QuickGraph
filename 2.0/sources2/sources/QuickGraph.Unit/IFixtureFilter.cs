using System;

namespace QuickGraph.Unit
{
    public interface IFixtureFilter
    {
        bool Filter(IFixture fixture);
    }
}
