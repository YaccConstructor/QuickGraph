using System;
using QuickGraph.Unit.Monitoring;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
        AllowMultiple=false, Inherited=true)]
    public sealed class DurationAttribute : TestDecoratorAttributeBase
    {
        private double maxDuration;

        public DurationAttribute(double maxDuration)
        {
            this.maxDuration = maxDuration;
        }

        public double MaxDuration
        {
            get { return this.maxDuration; }
        }

        public override ITestCase Decorate(ITestCase test)
        {
            return new DurationTestCase(test, this);
        }

        private sealed class DurationTestCase : TypeDecoratorTestCaseBase<DurationAttribute>
        {
            public DurationTestCase(ITestCase testCase, DurationAttribute attribute)
                :base(testCase, attribute)
            {}

            public override void Run(object fixture)
            {
                TimeMonitor monitor = new TimeMonitor();
                monitor.Start();
                this.TestCase.Run(fixture);
                monitor.Stop();

                Assert.IsLowerEqual(monitor.Duration, this.Attribute.MaxDuration);
            }
        }
    }
}
