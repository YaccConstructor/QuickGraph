using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TestAttribute : TestAttributeBase
    {
        public override IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method
            )
        {
            yield return new MethodTestCase(fixture.Name, method);
        }
    }
}
