using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Core
{
    [Serializable]
    public sealed class TestResultEventArgs : EventArgs
    {
        private TestResult result;
        public TestResultEventArgs(TestResult result)
        {
            if (result == null)
                throw new ArgumentNullException("result");
            this.result = result;
        }
        public TestResult Result
        {
            get { return this.result; }
        }
    }
}
