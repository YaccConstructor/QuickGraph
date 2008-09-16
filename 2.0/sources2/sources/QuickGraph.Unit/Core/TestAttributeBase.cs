using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickGraph.Unit.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class TestAttributeBase : Attribute, ITestCaseFactory
    {
        public abstract IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method
            );
    }
}
