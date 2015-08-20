using System;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=true, Inherited=true)]
    public sealed class SetEnvironmentVariableAttribute : TestDecoratorAttributeBase
    {
        private string name;
        private string value;

        public SetEnvironmentVariableAttribute(
            string name,
            string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Value
        {
            get { return this.value; }
        }

        public override ITestCase Decorate(ITestCase test)
        {
            return new SetEnvironmentVariableTestCase(test, this);
        }

        private sealed class SetEnvironmentVariableTestCase : TypeDecoratorTestCaseBase<SetEnvironmentVariableAttribute>
        {
            public SetEnvironmentVariableTestCase(
                ITestCase testCase,
                SetEnvironmentVariableAttribute attribute)
                :base(testCase,attribute)
            {}

            public override void Run(object fixture)
            {
                string value = null;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    value = Environment.GetEnvironmentVariable(this.Attribute.Name);
                    Console.WriteLine("Setting {0}={1}",
                        this.Attribute.Name,
                        this.Attribute.Value);
                    Environment.SetEnvironmentVariable(
                        this.Attribute.Name,
                        this.Attribute.Name);

                    this.TestCase.Run(fixture);
                }
                finally
                {
                    Environment.SetEnvironmentVariable(
                        this.Attribute.Name,
                        value);
                }
            }
        }
    }
}
