using System;
using System.Transactions;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=false, Inherited=true)]
    public sealed class RollbackAttribute : TestDecoratorAttributeBase
    {
        public override ITestCase Decorate(ITestCase testCase)
        {
            return new RolledbackTestCase(testCase);
        }

        private sealed class RolledbackTestCase : DecoratorTestCaseBase
        {
            public RolledbackTestCase(ITestCase testCase)
                :base(testCase)
            {}

            public override void Run(Object fixture)
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    this.TestCase.Run(fixture);
                }
            }
        }
    }
}
