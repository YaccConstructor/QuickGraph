using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited=true, AllowMultiple=true)]
    public sealed class MultiThreadedRepeatAttribute : TestDecoratorAttributeBase
    {
        private int threadCount = 1;

        public MultiThreadedRepeatAttribute(int threadCount)
        {
            this.threadCount = threadCount;
        }

        public int ThreadCount
        {
            get { return this.threadCount; }
            set { this.threadCount = value; }
        }

        public override ITestCase  Decorate(ITestCase testCase)
        {
            return new MultiThreadedRepeatTestCase(testCase, this);
        }

        private sealed class MultiThreadedRepeatTestCase : TypeDecoratorTestCaseBase<MultiThreadedRepeatAttribute>
        {
            private TestSynchronizer synchronizer;
            private TestCaseWorkerCollection workers = new TestCaseWorkerCollection();

            public MultiThreadedRepeatTestCase(ITestCase testCase, MultiThreadedRepeatAttribute attribute)
                :base(testCase, attribute)
            {
                this.synchronizer = new TestSynchronizer(this.Name);
            }

            public override void Run(object fixture)
            {
                // we block the synchronizer
                try
                {
                    this.synchronizer.Block();
                    // create the pool of threads
                    for (int i = 0; i < this.Attribute.ThreadCount; ++i)
                    {
                        SynchronizedTestCase synchronizedTest = new SynchronizedTestCase(this.TestCase, this.synchronizer);
                        workers.Add(new TestCaseWorker(synchronizedTest, i));
                    }
                    // starting threads
                    this.workers.StartAll(fixture);
                    // release barrier
                    this.synchronizer.Release();
                    // wait for finish and verify
                    this.workers.WaitAll();
                    this.workers.Verify();
                }
                finally
                {
                    // shutting down the synchronizer
                    this.synchronizer.Close();
                }
            }
        }
    }
}
