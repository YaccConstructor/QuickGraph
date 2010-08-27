using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public sealed class UsingEnumAttribute : UsingAttributeBase
    {
        public override void CreateDomains(
            IList<IDomain> domains,
            ParameterInfo parameter,
            IFixture fixture)
        {
			Assert.IsTrue(parameter.ParameterType.IsEnum,
				"Parameter {0} must be an enum", parameter.Name);
            ArrayDomain domain = new ArrayDomain(Enum.GetValues(parameter.ParameterType));
            domains.Add(domain);
        }
    }
}
