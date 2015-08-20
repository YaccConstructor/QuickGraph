using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    public interface IParameterDomainFactory
    {
        void CreateDomains(
            IList<IDomain> domains, 
            ParameterInfo parameter,
            IFixture fixture);
    }
}
