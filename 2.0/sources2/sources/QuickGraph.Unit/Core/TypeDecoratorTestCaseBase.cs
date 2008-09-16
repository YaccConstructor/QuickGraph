using System;

namespace QuickGraph.Unit.Core
{
    public abstract class TypeDecoratorTestCaseBase<A> : DecoratorTestCaseBase
        where A : Attribute
    {
        private A attribute;

        public TypeDecoratorTestCaseBase(ITestCase testCase, A attribute)
            :base(testCase)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");
            this.attribute = attribute;
        }

        public A Attribute
        {
            get { return this.attribute; }
        }
    }
}
