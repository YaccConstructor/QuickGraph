using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace QuickGraph.Unit.Core
{
    [Serializable]
    public class TestCaseWorker
    {
        private ITestCase testCase;
        private int index;
        private Exception throwedException = null;

        public TestCaseWorker(ITestCase testCase, int index)
        {
            if (testCase == null)
                throw new ArgumentNullException("testCase");
            this.testCase = testCase;
            this.index = index;
        }

        public ITestCase TestCase
        {
            get { return this.testCase; }
        }

        public int Index
        {
            get { return this.index;}
        }

        public Exception ThrowedException
        {
            get { return this.throwedException; }
        }

        public string Name
        {
            get { return String.Format("{0}{1}", this.TestCase.Name, this.Index);}
        }

        protected void Log(string format, params object[] args)
        {
            string message = String.Format(format, args);
            Console.WriteLine("{0}: {1}", this.Name, message);
        }

        public void Start(object fixture)
        {
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                this.RunTestCase(fixture);
            }
            catch (Exception ex)
            {
                Exception current = ex;
                if (current is TargetInvocationException)
                    current = current.InnerException;
                this.throwedException = current;
                Log("Exception {0}, {1}", current.GetType().Name, current.Message);
            }
            finally
            {
                CleanTestCase(fixture);
            }
        }

        protected virtual void RunTestCase(object fixture)
        {
            this.TestCase.Run(fixture);
        }

        protected virtual void CleanTestCase(object fixture)
        { }
    }
}
