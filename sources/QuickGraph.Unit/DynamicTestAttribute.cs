using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class DynamicTestAttribute : TestAttributeBase
    {
        public override IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method)
        {
            if (!typeof(IEnumerable<ITestCase>).IsAssignableFrom(method.ReturnType))
            {
                List<ITestCase> tests = new List<ITestCase>();
                tests.Add(new BadTestCase(
                    fixture.Name,
                    method.Name,
                    "A method tagged with DynamicTest must return IEnumerable<ITestCase>",
                    null
                    ));
                return tests;
            }

            // the fixture does not exist yet.
            Object fixtureInstance = null;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                fixtureInstance = fixture.CreateInstance();
                return (IEnumerable<ITestCase>)method.Invoke(fixtureInstance, null);
            }
            finally
            {
                IDisposable disposable = fixtureInstance as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }
    }
}
