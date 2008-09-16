using System;
using System.Reflection;
using System.Collections.Generic;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true, Inherited =true)]
    public sealed class TypeFixtureAttribute : TestFixtureAttributeBase
    {
        private Type testedType;

        public TypeFixtureAttribute(Type testedType)
        {
            if (testedType == null)
                throw new ArgumentNullException("testedType");
            this.testedType = testedType;
        }

        public Type TestedType
        {
            get { return this.testedType; }
        }

        public override IEnumerable<IFixture> CreateFixtures(Type fixtureType)
        {
            yield return new TypeFixture(this, fixtureType);
        }

        private sealed class TypeFixture : TypeFixtureBase<TypeFixtureAttribute>
        {
            public TypeFixture(TypeFixtureAttribute attribute, Type fixtureType)
                :base(attribute, fixtureType)
            {}

            public override IEnumerable<ITestCase> CreateTestCases()
            {
                Object[] attributes = this.FixtureType.GetCustomAttributes(typeof(TestCaseParameterFactoryAttributeBase),true);
                List<List<TestCaseParameter>> instances = new List<List<TestCaseParameter>>();
                List<List<ITestCase>> tests = new List<List<ITestCase>>();

                // first get the number of instances
                instances.Add(new List<TestCaseParameter>(this.CreateAllInstances(attributes)));
                int instanceCount = instances[0].Count;

                // get the number of tests
                tests.Add(new List<ITestCase>(CreateTestCaseInstances()));
                int testCount = tests[0].Count;

                // prepare for cross product
                for(int i=1;i<testCount;++i)
                    instances.Add(ToList(this.CreateAllInstances(attributes),instanceCount));
                for(int j=1;j<instanceCount;++j)
                    tests.Add(ToList(CreateTestCaseInstances(), testCount));

                // create tests cases
                for(int i=0;i<instanceCount;++i)
                {
                    for(int j=0;j<testCount;++j)
                    {
                        TestCaseParameter instance = instances[j][i];
                        ITestCase test = tests[i][j];
                        test.Parameters.Add(instance);
                        yield return test;
                    }
                }
            }

            private static List<T> ToList<T>(IEnumerable<T> en, int capacity)
            {
                List<T> list = new List<T>(capacity);
                list.AddRange(en);
                return list;
            }

            private IEnumerable<TestCaseParameter> CreateAllInstances(Object[] attributes)
            {
                foreach (TestCaseParameterFactoryAttributeBase atr in attributes)
                {
                    foreach (TestCaseParameter instance in atr.CreateInstances(
                            this.Attribute.TestedType
                            ))
                        yield return instance;
                }
            }

            private IEnumerable<ITestCase> CreateTestCaseInstances()
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
