using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    public abstract class TypeFixtureBase<A> : FixtureBase
        where A : TestFixtureAttributeBase
    {
        private A attribute;
        private Type fixtureType;
        public TypeFixtureBase(A attribute, Type fixtureType)
            :base(attribute.Apartment, attribute.TimeOut, attribute.Description)
        {
            if (fixtureType == null)
                throw new ArgumentNullException("fixtureType");

            this.attribute = attribute;
            this.fixtureType = fixtureType;
            this.IsCurrent = ReflectionHelper.GetAttribute<CurrentFixtureAttribute>(this.FixtureType) != null;
        }

        public A Attribute
        {
            get { return this.attribute; }
        }

        public Type FixtureType
        {
            get { return this.fixtureType; }
        }

        public override string Name
        {
            get { return this.FixtureType.FullName; }
        }

        public override object CreateInstance()
        {
            return Activator.CreateInstance(fixtureType);
        }

        protected ITestCase DecorateTest(Object[] decorators, ITestCase test)
        {
            ITestCase current= test;
            foreach (ITestCaseDecorator decorator in decorators)
                current = decorator.Decorate(current);
            return current;
        }
    }
}
