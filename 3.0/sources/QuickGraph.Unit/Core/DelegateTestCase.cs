using System;
using System.Collections.Generic;
using System.IO;

namespace QuickGraph.Unit.Core
{
    public class DelegateTestCase : TestCaseBase
    {
        private Delegate testDelegate;

        public DelegateTestCase(string fixtureName, Delegate testDelegate, params object[] args)
            : base(fixtureName)
        {
            this.testDelegate = testDelegate;
            foreach (Object arg in args)
                this.Parameters.Add(new TestCaseParameter(arg));
        }

        public Delegate TestDelegate
        {
            get { return this.testDelegate; }
        }

        public override string UndecoratedName
        {
            get { return this.testDelegate.Method.Name; }
        }

        public override void Run(Object fixture)
        {
            this.testDelegate.DynamicInvoke(TestCaseParameter.GetValues(this.Parameters));
        }
    }
}
