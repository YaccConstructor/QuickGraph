using System;
using System.Threading;
using System.Globalization;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class CulturesAttribute : TestDecoratorAttributeBase
    {
        private string cultures;

        public CulturesAttribute(string cultures)
        {
            if (String.IsNullOrEmpty(cultures))
                throw new ArgumentException("cultures");
            this.cultures = cultures;
        }

        public string[] GetCultures()
        {
            return this.cultures.Split(';');
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new CulturesTestCase(testCase, this);
        }

        private sealed class CulturesTestCase : TypeDecoratorTestCaseBase<CulturesAttribute>
        {
            public CulturesTestCase(ITestCase testCase, CulturesAttribute attribute)
                : base(testCase, attribute)
            { }

            public override void Run(object fixture)
            {
                CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    foreach (string cultureName in this.Attribute.GetCultures())
                    {
                        Console.WriteLine("Setting culture: {0}", cultureName);
                        CultureInfo culture = new CultureInfo(cultureName);
                        Thread.CurrentThread.CurrentCulture = culture;
                        this.TestCase.Run(fixture);
                    }
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = oldCulture;
                }
            }
        }
    }
}
