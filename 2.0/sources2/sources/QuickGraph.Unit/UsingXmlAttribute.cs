using System;
using System.Collections.Generic;
using QuickGraph.Operations;
using System.Reflection;

namespace QuickGraph.Unit
{
    public sealed class UsingXmlAttribute : UsingAttributeBase
    {
        private string resourceName;
        private string xPath;

        public UsingXmlAttribute(string resourceName, string xPath)
        {
            this.resourceName = resourceName;
            this.xPath = xPath;
        }

        public override void CreateDomains(IList<IDomain> domains, ParameterInfo parameter, IFixture fixture)
        {
            throw new NotImplementedException();
        }
    }
}
