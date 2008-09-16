using System;

namespace QuickGraph.Unit.Core
{
    public class SynchronizedTestCase : DecoratorTestCaseBase
    {
        private TestSynchronizer synchronizer;
        public SynchronizedTestCase(ITestCase testCase, TestSynchronizer synchronizer)
            : base(testCase)
        {
            this.synchronizer = synchronizer;
        }

        public TestSynchronizer Synchronizer
        {
            get { return this.synchronizer; }
        }

        public override void Run(object fixture)
        {
            // wait for barrier opening
            this.Synchronizer.Synchronize();
            // run test
            this.TestCase.Run(fixture);
        }
    }
}
