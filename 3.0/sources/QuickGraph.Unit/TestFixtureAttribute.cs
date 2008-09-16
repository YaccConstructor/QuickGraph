using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TestFixtureAttribute : TestFixtureAttributeBase 
    {
        public TestFixtureAttribute()
            : base()
        { }

        public override IEnumerable<IFixture> CreateFixtures(Type fixture)
        {
            yield return new TestFixture(this, fixture);   
        }

        private sealed class TestFixture : TypeFixtureBase<TestFixtureAttribute>
        {
            public TestFixture(TestFixtureAttribute attribute, Type fixtureType)
                :base(attribute, fixtureType)
            {}

            public override IEnumerable<ITestCase> CreateTestCases()
            {
                foreach (MethodInfo method in this.FixtureType.GetMethods())
                {
                    Object[] decorators = method.GetCustomAttributes(typeof(TestDecoratorAttributeBase), true);

                    foreach (TestAttributeBase attribute in method.GetCustomAttributes(typeof(TestAttributeBase), true))
                    {
                        foreach (ITestCase test in attribute.CreateTests(this, method))
                            yield return DecorateTest(decorators, test);
                    }
                }
            }
        }   
    }
}
