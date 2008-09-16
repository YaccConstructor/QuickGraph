using System;
using System.Collections.Generic;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public sealed class RepeatAttribute : TestDecoratorAttributeBase
    {
        private int count = 1;

        public RepeatAttribute()
        { }

        public RepeatAttribute(int count)
        {
            this.count = count;
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new RepeatTestCase(testCase, this);
        }

        private sealed class RepeatTestCase : TypeDecoratorTestCaseBase<RepeatAttribute>
        {
            public RepeatTestCase(ITestCase testCase, RepeatAttribute attribute)
                : base(testCase, attribute)
            { }

            public override void Run(object fixture)
            {
                for (int i = 0; i < this.Attribute.count; ++i)
                {
                    this.TestCase.Run(fixture);
                }
            }
        }
    }
}
