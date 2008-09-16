using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading;
using QuickGraph.Unit.Monitoring;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class MultiThreadedTestFixtureAttribute : TestFixtureAttributeBase
    {
        private int retryCount = 1;

        public int RetryCount
        {
            get { return this.retryCount; }
            set { this.retryCount = value; }
        }

        public override IEnumerable<IFixture> CreateFixtures(Type type)
        {
            foreach (MultiThreadedTestFixtureAttribute attribute in
                type.GetCustomAttributes(typeof(MultiThreadedTestFixtureAttribute), true))
            {
                yield return new MultiThreadedTestFixture(attribute, type);
            }
        }

        private sealed class MultiThreadedTestFixture : TypeFixtureBase<MultiThreadedTestFixtureAttribute>
        {
            public MultiThreadedTestFixture(MultiThreadedTestFixtureAttribute attribute, Type fixtureType)
                :base(attribute,fixtureType)
            {}

            public override IEnumerable<ITestCase> CreateTestCases()
            {
                // we create only 1 test case for this type of 
                // fixture
                MultiThreadedTestCase testCase = new MultiThreadedTestCase(this);

                // populate with threaded tests
                foreach (MethodInfo method in this.FixtureType.GetMethods())
                {
                    Object[] decorators = method.GetCustomAttributes(typeof(TestDecoratorAttributeBase), true);
                    foreach (MultiThreadedTestAttribute attribute in method.GetCustomAttributes(typeof(MultiThreadedTestAttribute), true))
                    {
                        // let's make sure the method takes a TestSynchronizer argument
                        if (!VerifyMethod(method))
                        {
                            yield return new BadTestCase(
                                this.Name,
                                method.Name,
                                "MultiThreadTest method requires TestSynchronizer arugment",
                                new Exception()
                                );
                            break;
                        }

                        int index = 0;
                        foreach (ITestCase test in attribute.CreateTests(this, method))
                        {
                            // we get the test case
                            ITestCase decoratedTest = DecorateTest(decorators, test);
                            // we add the synchronizer parameter
                            decoratedTest.Parameters.Add(new TestCaseParameter(testCase.Synchronizer));
                            // we add a worker for the test
                            TestCaseWorker worker = new TestCaseWorker(decoratedTest, index);
                            testCase.Workers.Add(worker);
                            index++;
                        }
                    }
                }

                yield return testCase;
            }

            private bool VerifyMethod(MethodInfo method)
            {
                ParameterInfo[] parameters = method.GetParameters();
                return parameters.Length == 1 
                    && parameters[0].ParameterType == typeof(TestSynchronizer);
            }
        }

        private sealed class MultiThreadedTestCase : TestCaseBase
        {
            private volatile TestSynchronizer synchronizer;
            private int retryCount;
            private TestCaseWorkerCollection workers = new TestCaseWorkerCollection();

            public MultiThreadedTestCase(MultiThreadedTestFixture fixture)
                : base(fixture.Name)
            {
                this.retryCount = fixture.Attribute.RetryCount;
                this.synchronizer = new TestSynchronizer(this.Name);
            }

            public TestSynchronizer Synchronizer
            {
                get { return this.synchronizer; }
            }

            public override string UndecoratedName
            {
	            get { return "Multi"; }
            }

            public TestCaseWorkerCollection Workers
            {
                get { return this.workers; }
            }

            public override void Run(object fixture)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                for (int i = 0; i < this.retryCount; ++i)
                {
                    if (i != 0)
                        Console.WriteLine("Multithread test retry {0}/{1}", i, retryCount);
                    this.InternalRun(fixture);
                }
            }

            private void InternalRun(object fixture)
            {
                // block barrier
                this.synchronizer.Block();

                RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    RuntimeHelpers.PrepareConstrainedRegions();
                    try
                    {
                        this.Workers.StartAll(fixture);
                    }
                    finally
                    {
                        // make sure the barrier is hit
                        while(!this.Workers.ContainsAll(this.synchronizer.GetThreadNames()))
                        {                            
                            Thread.Sleep(100);
                        }
                        // this will launch all the threads...
                        this.synchronizer.Release();
                    }

                    // runs threads
                    this.Workers.WaitAll();
                }
                finally
                {
                    // aborting thread if not dead yet
                    this.Workers.CloseAll();
                }

                // verifying execution
                this.Workers.Verify();
            }
        }
    }
}
