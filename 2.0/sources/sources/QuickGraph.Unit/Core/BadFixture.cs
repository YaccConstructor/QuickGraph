using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Core
{
    public sealed class BadFixture : FixtureBase
    {
        private string name;
        private string message;
        private Exception exception;

        public BadFixture(string name, Exception exception)
            : this(name, exception.Message)
        {
            this.exception = exception;
        }

        public BadFixture(string name, string message)
            : base(System.Threading.ApartmentState.Unknown,1, message)
        {
            this.name = name;
            this.message = message;
        }

        public override object CreateInstance()
        {
            return null;
        }

        public override string Name
        {
            get { return this.name; }
        }

        public string Message
        {
            get { return this.message; }
        }

        public Exception Exception
        {
            get { return this.exception; }
        }

        public override IEnumerable<ITestCase> CreateTestCases()
        {
            yield return new BadTestCase(
                this.Name,
                "ErrorLoadingFixture",
                this.message,
                this.Exception
                );
        }
    }
}
