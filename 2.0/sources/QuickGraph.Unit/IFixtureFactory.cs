using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickGraph.Unit
{
    public interface IFixtureFactory
    {
        IEnumerable<IFixture> CreateFixtures(Type type);
    }
}
