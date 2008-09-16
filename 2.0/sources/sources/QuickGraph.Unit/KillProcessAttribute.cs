using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
        AllowMultiple=true, Inherited=true)]
    public sealed class KillProcessAttribute : TestDecoratorAttributeBase
    {
        private string name;
        private NameMatchType matchType = NameMatchType.Exact;

        public KillProcessAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public NameMatchType MatchType
        {
            get { return this.matchType; }
            set { this.matchType = value; }
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new KillProcessDecoratorTestCase(testCase, this);
        }

        private sealed class KillProcessDecoratorTestCase : TypeDecoratorTestCaseBase<KillProcessAttribute>
        {
            public KillProcessDecoratorTestCase(ITestCase testCase, KillProcessAttribute attribute)
                : base(testCase,attribute)
            { }

            public override void Run(object fixture)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.TestCase.Run(fixture);
                }
                finally
                {
                    // looking for processes to kill
                    INameMatcher matcher = NameMatcherFactory.CreateMatcher(this.Attribute.MatchType, this.Attribute.Name);
                    foreach (Process p in Process.GetProcesses())
                    {
                        if (matcher.IsMatch(p.ProcessName))
                        {
                            Console.WriteLine("Killing {0} {1}", p.Id, p.ProcessName);
                            p.Kill();
                        }
                    }
                }
            }
        }
    }
}
