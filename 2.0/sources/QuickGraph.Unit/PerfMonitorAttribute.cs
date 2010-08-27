using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited=true, AllowMultiple=true)]
    public sealed class PerfMonitorAttribute : TestDecoratorAttributeBase
    {
        private string categoryName;
        private string counterName;
        private float valueDelta;
        private bool relativeDelta = true;

        public PerfMonitorAttribute(
            string categoryName,
            string counterName,
            float valueDelta
            )
        {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.valueDelta = valueDelta;
        }

        public string CategoryName
        {
            get { return this.categoryName; }
            set { this.categoryName = value; }
        }

        public string CounterName
        {
            get { return this.counterName; }
            set { this.counterName = value; }
        }

        public float ValueDelta
        {
            get { return this.valueDelta; }
            set { this.valueDelta = value; }
        }

        public bool RelativeDelta
        {
            get { return this.relativeDelta; }
            set { this.relativeDelta = value; }    
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new PerfMonitorTestCase(testCase, this);
        }

        private sealed class PerfMonitorTestCase : TypeDecoratorTestCaseBase<PerfMonitorAttribute>
        {
            public PerfMonitorTestCase(ITestCase testCase, PerfMonitorAttribute attribute)
                :base(testCase, attribute)
            {}

            public override void Run(object fixture)
            {
                using(PerformanceCounter counter = new PerformanceCounter(
                    this.Attribute.CategoryName,
                    this.Attribute.CounterName,
                    true))
                {
                    float startValue = counter.NextValue();
                    Console.WriteLine(
                        "[start] {0}/{1}: {2} [{3}]", 
                        this.Attribute.CategoryName, 
                        this.Attribute.CounterName, 
                        startValue,
                        counter.CounterType
                        );
                    this.TestCase.Run(fixture);
                    float endValue = counter.NextValue();
                    Console.WriteLine(
                        "[end] {0}/{1}: {2} [{3}]",
                        this.Attribute.CategoryName,
                        this.Attribute.CounterName,
                        endValue,
                        counter.CounterType
                        );

                    float delta = Math.Abs(endValue - startValue) ;
                    if (this.Attribute.RelativeDelta)
                        delta /= Math.Abs(startValue);

                    if (delta > this.Attribute.ValueDelta)
                        Assert.Fail("{0}/{1} value delta ({2}) is greater than accepted threshold ({3})",
                            this.Attribute.CategoryName,
                            this.Attribute.CounterName,
                            this.Attribute.ValueDelta,
                            delta);
                }
            }
        }
    }
}
