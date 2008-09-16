using System;
using System.Reflection;

namespace QuickGraph.Unit.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class TestDecoratorAttributeBase : Attribute, ITestCaseDecorator
    {
        public abstract ITestCase Decorate(ITestCase testCase);

        public static ITestCase DecorateTest(ITestCase testCase, MethodInfo method)
        {
            ITestCase test = testCase;
            foreach (TestDecoratorAttributeBase attribute in method.GetCustomAttributes(typeof(TestDecoratorAttributeBase), true))
            {
                test = attribute.Decorate(test);
            }
            return test;
        }
    }
}
