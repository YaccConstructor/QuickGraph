using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuickGraph.Unit
{
    public static class ReflectionAssert
    {
        public static void IsInstanceOfType(Type left, object value)
        {
            Assert.IsTrue(left.IsInstanceOfType(value),
                "{0} is not an instance of type {1}", left, value.GetType());
        }

        public static void IsAssignableFrom(Type left, Type right)
        {
            Assert.IsTrue(left.IsAssignableFrom(right),
                "Type {0} is not assignable from {1}", left,right);
        }

        public static void IsSealed(Type type)
        {
            Assert.IsTrue(type.IsSealed,
                "Type {0} is not sealed", type);
        }

        public static void HasConstructor(Type type, params Type[] parameterTypes)
        {
            Assert.IsNotNull(
                type.GetConstructor(parameterTypes),
                "Type {0} has no suitable constructor", type);
        }

        public static void IsTaggedWithAttribute(
            ICustomAttributeProvider type,
            Type attributeType,
            bool inherit)
        {
            Object[] attributes = type.GetCustomAttributes(attributeType, inherit);
            if (attributes == null || attributes.Length == 0)
                Assert.Fail(
                    "{0} is not tagged with {1}", type, attributeType);
        }

        public static void DisplayType(Type type)
        {
            Console.WriteLine("FullName: {0}", type.FullName);
            Console.WriteLine("Base: {0}", type.BaseType);
            Type[] interfaceTypes = type.GetInterfaces();
            Console.WriteLine("Interfaces ({0}):", interfaceTypes.Length);
            foreach (Type interfaceType in interfaceTypes)
                Console.WriteLine("\t{0}", interfaceType);

            ConstructorInfo[] constructors = type.GetConstructors();
            Console.WriteLine("Constructors ({0}):", constructors.Length);
            foreach (ConstructorInfo constructor in constructors)
            {
                Console.WriteLine("\t{0}({1})", constructor.Name,
                    ParametersToString(constructor.GetParameters())
                );
            }

            MethodInfo[] methods = type.GetMethods();
            Console.WriteLine("Methods ({0}):", methods.Length);
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine("\t{0}({1})", method.Name,
                    ParametersToString(method.GetParameters())
                );
            }
        }

        public static string ParametersToString(ParameterInfo[] parameters)
        {
            using (StringWriter writer = new StringWriter())
            {
                foreach (ParameterInfo parameter in parameters)
                    writer.Write(",{0} {1}", parameter.ParameterType, parameter.Name);

                return String.Format("{0}", writer.ToString().TrimStart(','));
            }
        }

    }
}
