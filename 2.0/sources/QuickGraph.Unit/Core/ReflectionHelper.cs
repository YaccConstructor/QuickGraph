using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuickGraph.Unit.Core
{
    public static class ReflectionHelper
    {
        public static Object[] ToArray(ICollection<Object> parameters)
        {
            Object[] array = new object[parameters.Count];
            parameters.CopyTo(array, 0);
            return array;
        }

        public static T GetAttribute<T>(ICustomAttributeProvider t)
            where T : Attribute
        {
            if (t == null)
                throw new ArgumentNullException("t");

            // Gets the attributes for the property.
            Object[] attributes =
               t.GetCustomAttributes(typeof(T), true);

            if (attributes.Length == 0)
                return null;
            if (attributes.Length==1)
                return (T)attributes[0];

            throw new ArgumentException("Attribute type must be AllowMultiple = false",typeof(T).FullName);
        }

        public static string GetCategory(ICustomAttributeProvider t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            System.ComponentModel.CategoryAttribute cat = ReflectionHelper.GetAttribute < System.ComponentModel.CategoryAttribute>(t);
            if (cat == null)
                return "";
            return cat.Category;
        }

        public static MethodInfo GetMethod(Type t, string methodName,params Object[] parameters)
        {
            if (t == null)
                throw new ArgumentNullException("t");
            if (methodName == null)
                throw new ArgumentNullException("methodName");
            if (methodName.Length == 0)
                throw new ArgumentException("Length is zero", "methodName");

            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i)
            {
                if (parameters[i] == null)
                    throw new ArgumentNullException("paramer[" + i.ToString() + "]");
                types[i] = parameters[i].GetType();
            }

            return t.GetMethod(methodName,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic,
                null,
                types,
                null
                );
        }

        public static MethodInfo GetMethod(Type t, Type customAttributeType, BindingFlags flags)
        {
            MethodInfo[] methods = GetMethods(t, customAttributeType, flags);
            if (methods.Length == 0)
                return null;
            if (methods.Length > 1)
                throw new ArgumentException("More that one method found");
            return methods[0];
        }

        public static MethodInfo[] GetMethods(Type t, Type customAttributeType)
        {
            return GetMethods(t, customAttributeType, BindingFlags.Public | BindingFlags.Instance);
        }

        public static MethodInfo[] GetMethods(Type t, Type customAttributeType, BindingFlags flags)
        {
            if (t == null)
                throw new ArgumentNullException("t");
            if (customAttributeType == null)
                throw new ArgumentNullException("customAttributeType");

            List<MethodInfo> list = new List<MethodInfo>();
            foreach (MethodInfo mi in t.GetMethods(flags))
            {
                if (mi.GetCustomAttributes(customAttributeType, true).Length != 0)
                    list.Add(mi);
            }

            MethodInfo[] methods = new MethodInfo[list.Count];
            list.CopyTo(methods);
            return methods;
        }


        public static void VerifySignature(MethodInfo method, params Type[] argumentAssignableTypes)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            ParameterInfo[] parameters = method.GetParameters();
            Assert.AreEqual(
                argumentAssignableTypes.Length,
                parameters.Length,
                "Method {0} has not the right number of arguments ({1})",
                method.Name, argumentAssignableTypes.Length);

            for (int i = 0; i < parameters.Length; ++i)
            {
                Assert.IsTrue(argumentAssignableTypes[i].IsAssignableFrom(parameters[i].ParameterType),
                    "Argument {0} is not assignable from {1}",
                    argumentAssignableTypes[i],
                    parameters[i]
                    );
            }
        }

        public static void VerifySignature(MethodInfo method, params Object[] arguments)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (arguments == null)
            {
                VerifySignature(method, new Type[]{});
                return;
            }
            Type[] types = new Type[arguments.Length];
            for (int i = 0; i < arguments.Length; ++i)
            {
                if (arguments[i] == null)
                    types[i] = typeof(object);
                else
                    types[i] = arguments[i].GetType();
            }

            VerifySignature(method, types);
        }
    }
}
