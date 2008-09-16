using System;

namespace QuickGraph.Unit.Core
{
    public abstract class TypeTestCaseBase<A> : TestCaseBase
        where A : Attribute
    {
        private A attribute;

        public TypeTestCaseBase(string fixtureName, A attribute)
            : base(fixtureName)
        {
            this.attribute = attribute;
        }

        public A Attribute
        {
            get { return this.attribute; }
        }
    }
}
