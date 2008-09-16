using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter,AllowMultiple =true,Inherited =true)]
    public abstract class UsingAttributeBase :
        Attribute, 
        IParameterDomainFactory
    {
        public abstract void CreateDomains(
            IList<IDomain> domains,
            ParameterInfo parameter,
            IFixture fixture);
    }
}
