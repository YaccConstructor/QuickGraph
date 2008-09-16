using System;
using System.Reflection;
using System.Collections.Generic;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public sealed class UsingLiteralsAttribute : UsingAttributeBase
    {
        private string values;

        public UsingLiteralsAttribute(string values)
        {
            this.values = values;
        }

        /// <summary>
        /// Gets a list of values separated by ;
        /// </summary>
        /// <value></value>
        public string Values
        {
            get
            {
                return this.values;
            }
        }

        public override void CreateDomains(
            IList<IDomain> domains, 
            ParameterInfo parameter, 
            IFixture fixture)
        {
            bool isString = parameter.ParameterType.IsAssignableFrom(typeof(string));
            List<Object> data = new List<Object>();
            foreach (string memberName in this.Values.Split(';'))
            {
                object cresult = null;
                if (isString)
                    cresult = memberName.ToString();
                else
                    cresult = Convert.ChangeType(memberName, parameter.ParameterType);
                data.Add(cresult);
            }
            if (data.Count == 0)
                return;

            CollectionDomain domain = new CollectionDomain(data);
            domains.Add(domain);
        }
    }
}
