using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Core
{
    public abstract class DecoratorTestCaseBase : ITestCase
    {
        private ITestCase testCase;
        public DecoratorTestCaseBase(ITestCase testCase)
        {
            if (testCase == null)
                throw new ArgumentNullException("testCase");
            this.testCase = testCase;
        }

        public ITestCase TestCase
        {
            get { return this.testCase; }
        }

        public string FullName
        {
            get { return String.Format("{0}.{1}", this.FixtureName, this.Name); }
        }

        public virtual string Name
        {
            get { return this.TestCase.Name; }
        }

        public string FixtureName
        {
            get { return this.testCase.FixtureName; }
        }

        public IList<TestCaseParameter> Parameters
        {
            get { return this.TestCase.Parameters; }
        }

        public abstract void Run(Object fixture);
    }
}
