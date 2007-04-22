using System;
using QuickGraph.Unit.Exceptions;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class IgnoreAttribute : TestDecoratorAttributeBase
    {
        private string message;

        public IgnoreAttribute(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.message = message;
        }

        public string Message
        {
            get { return this.message; }
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new IgnoredTestCase(testCase, this.Message);
        }

        private sealed class IgnoredTestCase : DecoratorTestCaseBase
        {
            private string message;
            public IgnoredTestCase(ITestCase testCase, string message)
                : base(testCase)
            {
                this.message = message;
            }

            public override void Run(Object fixture)
            {
                throw new IgnoreException(this.message);
            }
        }
    }
}
