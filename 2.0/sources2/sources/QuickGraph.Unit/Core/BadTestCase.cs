using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Core
{
    public sealed class BadTestCase : ITestCase
    {
        private string fixtureName;
        private string name;
        private string message;
        private Exception exception;
        private List<TestCaseParameter> parameters = new List<TestCaseParameter>();

        public BadTestCase(
            string fixtureName,
            string name,
            string message,
            Exception exception
            )
        {
            this.fixtureName = fixtureName;
            this.name = name;
            this.message = message;
            this.exception = exception;
        }

        public string FixtureName 
        {
            get { return this.fixtureName; }
        }
        public string Name 
        {
            get { return this.name; }
        }
        public string FullName
        {
            get { return String.Format("{0}.{1}", this.FixtureName, this.Name); }
        }
        public IList<TestCaseParameter> Parameters 
        {
            get { return this.parameters; }
        }

        public void Run(Object fixture)
        {
            if (this.exception!=null)
                throw new Exceptions.FixtureReflectionFailedException(
                    this.message,
                    this.exception);
            else
                throw new Exceptions.FixtureReflectionFailedException(
                    this.message);
        }
    }
}
