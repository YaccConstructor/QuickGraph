using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickGraph.Unit
{
    public interface ITestCaseFactory
    {
        IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method
            );
    }
}
