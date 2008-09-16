using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class UsingBooleanAttribute : UsingAttributeBase
    {
        public UsingBooleanAttribute()
        { }

        public override void CreateDomains(
            IList<IDomain> domains, 
            ParameterInfo parameter, 
            IFixture fixture)
        {
            domains.Add(Domains.Boolean);
        }
    }
}
