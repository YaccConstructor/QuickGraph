using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true, Inherited =true)]
    public sealed class TypeFactoryAttribute : TestCaseParameterFactoryAttributeBase
    {
        private Type factoryType;

        public TypeFactoryAttribute(Type factoryType)
        {
            if (factoryType == null)
                throw new ArgumentNullException("factoryType");
            this.factoryType = factoryType;
        }

        public Type FactoryType
        {
            get { return this.factoryType; }
        }

        public override IEnumerable<TestCaseParameter> CreateInstances(Type targetType)
        {
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            foreach (MethodInfo method in ReflectionHelper.GetMethods(this.FactoryType, typeof(FactoryAttribute)))
            {
                if (method.GetParameters().Length > 0)
                    continue;

                FactoryAttribute factoryAttribute = ReflectionHelper.GetAttribute<FactoryAttribute>(method);

                // the method returns the type or a enumerable collection
                if (targetType.IsAssignableFrom(method.ReturnType))
                    yield return CreateSingleInstance(method);
                else if (
                       targetType.IsAssignableFrom(typeof(IEnumerable<>).MakeGenericType(targetType))
                    || (
                            targetType.IsAssignableFrom(typeof(IEnumerable))
                           && targetType.IsAssignableFrom(factoryAttribute.FactoredType)
                        )
                    )
                {
                    foreach (TestCaseParameter parameter in CreateMultipleInstances(method))
                        yield return parameter;
                }
            }
        }

        private TestCaseParameter CreateSingleInstance(MethodInfo method)
        {
            object instance = null;
            object factory = null;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                factory = Activator.CreateInstance(this.FactoryType);
                instance = method.Invoke(factory, null);
            }
            finally
            {
                IDisposable disposable = factory as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }

            return new TestCaseParameter(
                String.Format("{0}.{1}",this.FactoryType.Name,method.Name),
                instance
                );
        }

        private IEnumerable<TestCaseParameter> CreateMultipleInstances(MethodInfo method)
        {
            System.Collections.IEnumerable instances = null;
            object factory = null;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                factory = Activator.CreateInstance(this.FactoryType);
                instances = method.Invoke(factory, null) as IEnumerable;

                int index = 0;
                foreach (object instance in instances)
                {
                    yield return new TestCaseParameter(
                        String.Format("{0}.{1}.{2}", this.FactoryType.Name, method.Name, index++),
                        instance
                        );
                }
            }
            finally
            {
                IDisposable disposable = factory as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }
    }
}
