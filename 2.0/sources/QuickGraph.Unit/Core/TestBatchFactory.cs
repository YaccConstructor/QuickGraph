using System;
using System.Collections.Generic;
using System.Reflection;
using System.CodeDom.Compiler;
using QuickGraph.Unit.Exceptions;
using QuickGraph.Unit.Filters;
using System.ComponentModel;

namespace QuickGraph.Unit.Core
{
    public sealed class TestBatchFactory
    {
        private List<Assembly> testAssemblies = new List<Assembly>();
        private TestBatch batch;
        private IFixtureFilter fixtureFilter = new AnyFixtureFilter();
        private ITestCaseFilter testCaseFilter = new AnyTestCaseFilter();

        public TestBatch Batch
        {
            get { return this.batch; }
        }

        public IList<Assembly> TestAssemblies
        {
            get { return this.testAssemblies; }
        }

        public string MainAssembly
        {
            get
            {
                if (this.testAssemblies.Count == 0)
                    return "NoAssemblies";
                else
                    return this.testAssemblies[0].GetName().Name;
            }
        }

        public IFixtureFilter FixtureFilter
        {
            get { return this.fixtureFilter; }
            set { this.fixtureFilter = value; }
        }

        public ITestCaseFilter TestCaseFilter
        {
            get { return this.testCaseFilter; }
            set { this.testCaseFilter = value; }
        }

        public TestBatch Create()
        {
            this.batch = new TestBatch();
            foreach (Assembly assembly in this.TestAssemblies)
            {
                TestAssembly testAssembly = CreateTestAssembly(assembly);
                this.batch.TestAssemblies.Add(testAssembly);
            }
            return this.batch;
        }

        public TestAssembly CreateTestAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            TestAssembly testAssembly = new TestAssembly(assembly);
            this.ReflectAssemblySetUpAndTearDown(testAssembly);

            foreach (Type type in assembly.GetExportedTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;
                foreach (IFixture fixture in this.ReflectFixtures(type))
                    AddFixture(testAssembly, fixture);
            }

            return testAssembly;
        }

        private void AddFixture(TestAssembly testAssembly, IFixture fixture)
        {
            List<ITestCase> tests = new List<ITestCase>();

            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                foreach (ITestCase test in fixture.CreateTestCases())
                {
                    if (test == null)
                        continue;
                    if (!testCaseFilter.Filter(fixture,test))
                        continue;
                    ITestCase decoratedTest = DecorateTest(fixture, test);
                    tests.Add(decoratedTest);
                }
                testAssembly.AddFixture(fixture, tests);
            }
            catch (Exception ex)
            {
                BadFixture badFixture = new BadFixture(fixture.Name, ex);
                tests.Clear();
                testAssembly.AddFixture(badFixture, tests);
            }
        }

        private static ITestCase DecorateTest(IFixture fixture, ITestCase test)
        {
            ITestCase current = test;
            foreach (ITestCaseDecorator decorator in fixture.TestCaseDecorators)
                current = decorator.Decorate(current);
            return current;
        }

        private IEnumerable<IFixture> ReflectFixtures(Type fixtureType)
        {
            foreach(TestFixtureAttributeBase fixtureAttribute in fixtureType.GetCustomAttributes(typeof(TestFixtureAttributeBase),true))
            {
                foreach (IFixture fixture in fixtureAttribute.CreateFixtures(fixtureType))
                {
                    if (fixture == null)
                        continue;
                    if (!this.FixtureFilter.Filter(fixture))
                        continue;

                    BadFixture badFixture = VerifyFixture(fixtureType);
                    if (badFixture != null)
                    {
                        yield return badFixture;
                        continue;
                    }

                    ReflectSetUpAndTearDown(fixtureType, fixture);
                    ReflectDecorators(fixtureType, fixture);
                    ReflectCategories(fixtureType, fixture);

                    yield return fixture;
                }
            }
        }

        private static BadFixture VerifyFixture(Type fixtureType)
        {
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                MethodInfo ms = FilterMethod(method, typeof(TestFixtureSetUpAttribute));
                if (ms != null)
                    return new BadFixture(fixtureType.FullName, "TestFixtureSetupAttribute must tag static methods");
                MethodInfo mt = FilterMethod(method, typeof(TestFixtureTearDownAttribute));
                if (mt != null)
                    return new BadFixture(fixtureType.FullName, "TestFixtureTearDownAttribute must tag static methods");
            }
            return null;
        }

        private void ReflectAssemblySetUpAndTearDown(TestAssembly testAssembly)
        {
            AssemblySetUpAndTearDownAttribute assemblySetUpAndTearDown =
                ReflectionHelper.GetAttribute<AssemblySetUpAndTearDownAttribute>(testAssembly.Assembly);
            if (assemblySetUpAndTearDown != null)
            {
                testAssembly.AssemblySetUp = ReflectionHelper.GetMethod(
                    assemblySetUpAndTearDown.TargetType,
                    typeof(AssemblySetUpAttribute),
                    BindingFlags.Static | BindingFlags.Public
                    );
                testAssembly.AssemblyTearDown = ReflectionHelper.GetMethod(
                    assemblySetUpAndTearDown.TargetType,
                    typeof(AssemblyTearDownAttribute),
                    BindingFlags.Static | BindingFlags.Public
                    );
            }

        }

        private void ReflectDecorators(Type fixtureType, IFixture fixture)
        {
            foreach (TestDecoratorAttributeBase attribute in fixtureType.GetCustomAttributes(typeof(TestDecoratorAttributeBase), true))
                fixture.TestCaseDecorators.Add(attribute);
        }

        private void ReflectCategories(Type fixtureType, IFixture fixture)
        {
            CategoryAttribute category = ReflectionHelper.GetAttribute<CategoryAttribute>(fixtureType);
            if (category != null)
            {
                foreach (string cat in category.Category.Split('.'))
                    fixture.Categories.Add(cat);
            }
        }

        private static void ReflectSetUpAndTearDown(Type fixtureType, IFixture fixture)
        {
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                if (fixture.FixtureSetUp == null)
                {
                    fixture.FixtureSetUp = FilterMethod(method, typeof(TestFixtureSetUpAttribute));
                    if (fixture.FixtureSetUp != null)
                        continue;
                }
                if (fixture.FixtureTearDown == null)
                {
                    fixture.FixtureTearDown = FilterMethod(method, typeof(TestFixtureTearDownAttribute));
                    if (fixture.FixtureTearDown != null)
                        continue;
                }
            }

            // getting setup and teardown
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                if (fixture.SetUp == null)
                {
                    fixture.SetUp = FilterMethod(method, typeof(SetUpAttribute));
                    if (fixture.SetUp != null)
                        continue;
                }
                if (fixture.TearDown == null)
                {
                    fixture.TearDown = FilterMethod(method, typeof(TearDownAttribute));
                    if (fixture.TearDown != null)
                        continue;
                }
            }
        }

        private static MethodInfo FilterMethod(MethodInfo method, Type attributeType)
        {
            Object[] attributes = method.GetCustomAttributes(attributeType, true);
            if (attributes.Length == 0)
                return null;

            return method;
        }
    }
}
