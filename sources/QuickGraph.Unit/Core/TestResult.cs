using System;
using System.Reflection;

namespace QuickGraph.Unit.Core
{
    [Serializable]
    public class TestResult : Result
    {
        private string fixtureName;

        public TestResult(string fixtureName, MethodInfo method)
            :this(fixtureName,method.Name)
        {}

        public TestResult(string fixtureName, string name)
            :base(name)
        {
            if (fixtureName == null)
                throw new ArgumentNullException("fixtureName");
            this.fixtureName = fixtureName;            
        }

        public TestResult(ITestCase testCase)
            : this(testCase.FixtureName, testCase.Name)
        {
        }

        public string FixtureName
        {
            get { return this.fixtureName; }
        }

        public override string FullName
        {
            get { return String.Format("{0}.{1}", this.FixtureName, this.Name); }
        }
    }
}
