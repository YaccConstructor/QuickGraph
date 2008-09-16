using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public sealed class UsingImplementationsAttribute : UsingAttributeBase
    {
        private Type typeFromAssembly;

        public UsingImplementationsAttribute(Type typeFromAssembly)
        {
            if (typeFromAssembly == null)
                throw new ArgumentNullException("typeFromAssembly");
            this.typeFromAssembly = typeFromAssembly;
        }

        public override void CreateDomains(
            IList<IDomain> domains,
            ParameterInfo parameter, 
            IFixture fixture)
        {
            List<Object> types = new List<Object>();
            foreach (Type type in typeFromAssembly.Assembly.GetExportedTypes())
            {
                if (type.IsAbstract || type.IsInterface || !type.IsClass)
                    continue;

                if (!parameter.ParameterType.IsAssignableFrom(type))
                    continue;

                // create instance
                Object instance = Activator.CreateInstance(type);
                types.Add(instance);
            }

            CollectionDomain domain = new CollectionDomain(types);
            domain.Name = typeFromAssembly.Assembly.GetName().Name;
            domains.Add(domain);
        }
    }
}
