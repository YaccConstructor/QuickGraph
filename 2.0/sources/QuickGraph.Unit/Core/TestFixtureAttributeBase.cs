using System;
using System.Collections.Generic;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class TestFixtureAttributeBase : 
        Attribute,
        IFixtureFactory
    {
        private ApartmentState apartment = ApartmentState.Unknown;
        private int timeOut = 2;
        private string description;

        public ApartmentState Apartment
        {
            get { return this.apartment; }
            set { this.apartment = value; }
        }

        public int TimeOut
        {
            get { return this.timeOut; }
            set { this.timeOut = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public abstract IEnumerable<IFixture> CreateFixtures(Type type);
    }
}
