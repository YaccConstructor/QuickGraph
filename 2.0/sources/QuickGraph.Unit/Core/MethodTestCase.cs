using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuickGraph.Unit.Core
{
    public class MethodTestCase : TestCaseBase
    {
        private MethodInfo method;

        public MethodTestCase(string fixtureName, MethodInfo method)
            :base(fixtureName)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            this.method = method;
        }

        public MethodInfo Method
        {
            get { return this.method; }
        }

        public override string UndecoratedName
        {
            get { return this.Method.Name; }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Run(Object fixture)
        {
            if (this.Parameters.Count == 0)
                this.method.Invoke(fixture, null);
            else
            {
                Object[] array = TestCaseParameter.GetValues(this.Parameters);
                this.method.Invoke(fixture, array);
            }
        }
    }
}
