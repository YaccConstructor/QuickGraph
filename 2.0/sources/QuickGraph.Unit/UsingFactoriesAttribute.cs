using System;
using System.Collections.Generic;
using QuickGraph.Operations;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(
        AttributeTargets.Parameter,
        AllowMultiple = true, 
        Inherited = true)]
    public sealed class UsingFactoriesAttribute : UsingAttributeBase
    {
        private Type factoryType = null;
        public UsingFactoriesAttribute(Type factoryType)
        {
            if (factoryType == null)
                throw new ArgumentNullException("factoryType");
            this.factoryType = factoryType;
        }

        public Type FactoryType
        {
            get
            {
                return this.factoryType;
            }
            set
            {
                this.factoryType = value;
            }
        }

        public override void CreateDomains(
            IList<IDomain> domains,
            ParameterInfo parameter, 
            IFixture fixture)
        {
            Object factory = null;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                factory = Activator.CreateInstance(this.FactoryType);
                this.GetAllDomains(domains, parameter, factory);
            }
            finally
            {
                IDisposable disposable = factory as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        private void GetAllDomains(
            IList<IDomain> domains, 
            ParameterInfo parameter, 
            object factory)
        {
            foreach (MethodInfo factoryMethod in ReflectionHelper.GetMethods(factory.GetType(), typeof(FactoryAttribute)))
            {
                if (factoryMethod.GetParameters().Length > 0)
                    continue;
                Type returnType = factoryMethod.ReturnType;

                // check single object return
                if (parameter.ParameterType.IsAssignableFrom(returnType))
                {
                    object result = factoryMethod.Invoke(factory, null);
                    IDomain domain = Domains.ToDomain(result);
                    domain.Name = factoryMethod.Name;
                    domains.Add(domain);
                    continue;
                }

                // check array
                if (returnType.HasElementType)
                {
                    Type elementType = returnType.GetElementType();
                    if (parameter.ParameterType == elementType)
                    {
                        object result = factoryMethod.Invoke(factory,null);
                        IDomain domain = Domains.ToDomain(result);
                        domain.Name = factoryMethod.Name;
                        domains.Add(domain);
                        continue;
                    }
                }

                // check IEnumerable
                if (returnType.IsGenericType)
                {
                    if (typeof(IEnumerable<>).IsAssignableFrom(returnType.GetGenericTypeDefinition()))
                    {
                        if (parameter.ParameterType.IsAssignableFrom(returnType.GetGenericArguments()[0]))
                        {
                            object result = factoryMethod.Invoke(factory, null);
                            IDomain domain = Domains.ToDomain(result);
                            domain.Name = factoryMethod.Name;
                            domains.Add(domain);
                            continue;
                        }
                    }
                }

                // check factory type
                FactoryAttribute factoryAttribute = ReflectionHelper.GetAttribute<FactoryAttribute>(factoryMethod);
                if (factoryAttribute != null)
                {
                    Type factoredType = factoryAttribute.FactoredType;
                    if (parameter.ParameterType == factoredType)
                    {
                        object result = factoryMethod.Invoke(factory, null);
                        IDomain domain = Domains.ToDomain(result);
                        domain.Name = factoryMethod.Name;
                        domains.Add(domain);
                        continue;
                    }
                }
            }
        }
    }
}
