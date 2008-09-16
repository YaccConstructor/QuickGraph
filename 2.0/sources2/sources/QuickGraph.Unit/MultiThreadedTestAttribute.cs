using System;
using System.Reflection;
using System.Collections.Generic;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class MultiThreadedTestAttribute : TestAttributeBase
    {
        private int threadCount = 1;

        public MultiThreadedTestAttribute()
        { }

        public MultiThreadedTestAttribute(int threadCount)
        {
            this.threadCount = threadCount;
        }

        public int ThreadCount
        {
            get { return this.threadCount; }
            set { this.threadCount = value; }
        }

        public override IEnumerable<ITestCase> CreateTests(IFixture fixture, MethodInfo method)
        {
            for (int i = 0; i < this.ThreadCount; ++i)
            {
                yield return new MethodTestCase(fixture.Name, method);
            }
        }
    }
}
