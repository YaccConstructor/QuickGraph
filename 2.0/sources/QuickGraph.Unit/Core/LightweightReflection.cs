using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace QuickGraph.Unit.Core
{
    public delegate void EmptyInvoker();
    public delegate T CoVariantInvoker<T>();
    public delegate void ContraVariantInvoker<T>(T value);

    public static class LightweightReflection
    {
        #region CreatePropertySetData
        private static object syncPropertySetDataRoot = new object();
        private static Dictionary<PropertyInfo, DynamicMethod> propertySetDatas = new Dictionary<PropertyInfo, DynamicMethod>();

        public static T CreateSetData<T>(PropertyInfo property)
        {
            return (T)(Object)CreateSetData(property).CreateDelegate(typeof(T));
        }

        public static T CreateSetData<T>(PropertyInfo property, object target)
        {
            return (T)(Object)CreateSetData(property).CreateDelegate(typeof(T), target);
        }

        public static DynamicMethod CreateSetData(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (!property.CanWrite)
                throw new ArgumentException("property cannot set data");

            DynamicMethod dynamicMethod;
            lock (syncPropertySetDataRoot)
            {
                if (propertySetDatas.TryGetValue(property, out dynamicMethod))
                    return dynamicMethod;

                string name = "Set" + property.MetadataToken.ToString();
                Module module = property.Module;
                Type[] parameterTypes = GetParameterTypes(property.GetSetMethod());
                dynamicMethod = new DynamicMethod(
                    name,
                    null,
                    parameterTypes,
                    module);
                GenerateInvokeBody(property.GetSetMethod(),
                    dynamicMethod.GetILGenerator(),
                    parameterTypes);
                propertySetDatas.Add(property, dynamicMethod);
                return dynamicMethod;
            }
        }

        private static void GeneratePropertySetDataBody(
            PropertyInfo property,
            ILGenerator gen,
            Type[] parameters
            )
        {
            MethodInfo setMethod = property.GetSetMethod();

            for (int i = 0; i < parameters.Length; ++i)
                gen.Emit(OpCodes.Ldarg, i);

            OpCode callCode;
            if (setMethod.IsVirtual)
                callCode = OpCodes.Callvirt;
            else
                callCode = OpCodes.Call;
            gen.Emit(callCode, setMethod);
            gen.Emit(OpCodes.Ret);
        }
        #endregion

        #region CreatePropertyGetData
        private static object syncPropertyGetDataRoot = new object();
        private static Dictionary<PropertyInfo, DynamicMethod> propertyGetDatas = new Dictionary<PropertyInfo, DynamicMethod>();

        public static T CreateGetData<T>(PropertyInfo property)
        {
            return (T)(Object)CreateGetData(property).CreateDelegate(typeof(T));
        }

        public static T CreateGetData<T>(PropertyInfo property, object target)
        {
            return (T)(Object)CreateGetData(property).CreateDelegate(typeof(T), target);
        }

        public static DynamicMethod CreateGetData(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (!property.CanRead)
                throw new ArgumentException("property cannot read");

            DynamicMethod dynamicMethod;
            lock (syncPropertyGetDataRoot)
            {
                if (propertyGetDatas.TryGetValue(property, out dynamicMethod))
                    return dynamicMethod;

                string name = "Get" + property.MetadataToken.ToString();
                Module module = property.DeclaringType.Module;
                Type returnType = property.PropertyType;
                Type[] parameterTypes = GetParameterTypes(property.GetGetMethod());

                dynamicMethod = new DynamicMethod(
                    name,
                    returnType,
                    parameterTypes,
                    module);
                GenerateInvokeBody(property.GetGetMethod(),
                    dynamicMethod.GetILGenerator(),
                    parameterTypes);
                propertyGetDatas.Add(property, dynamicMethod);
                return dynamicMethod;
            }

        }
        #endregion

        #region CreateFieldSetData
        private static object syncFieldSetDataRoot = new object();
        private static Dictionary<FieldInfo, DynamicMethod> fieldSetDatas = new Dictionary<FieldInfo, DynamicMethod>();

        public static T CreateSetData<T>(FieldInfo field)
        {
            return (T)(Object)CreateSetData(field).CreateDelegate(typeof(T));
        }

        public static T CreateSetData<T>(FieldInfo field, object target)
        {
            return (T)(Object)CreateSetData(field).CreateDelegate(typeof(T), target);
        }

        public static DynamicMethod CreateSetData(FieldInfo field)
        {
            if (field == null)
                throw new ArgumentNullException("field");
            DynamicMethod dynamicMethod;
            lock (syncFieldSetDataRoot)
            {
                if (fieldSetDatas.TryGetValue(field, out dynamicMethod))
                    return dynamicMethod;

                string name = "Set" + field.MetadataToken.ToString();
                Module module = field.DeclaringType.Module;
                Type[] parameterTypes =
                    (field.IsStatic) ? new Type[] { field.FieldType }
                    : new Type[] { field.DeclaringType, field.FieldType };

                dynamicMethod = new DynamicMethod(
                    name,
                    null,
                    parameterTypes,
                    module);
                GenerateFieldSetDataBody(
                    field,
                    dynamicMethod.GetILGenerator());
                fieldSetDatas.Add(field, dynamicMethod);
                return dynamicMethod;
            }
        }

        private static void GenerateFieldSetDataBody(
            FieldInfo field,
            ILGenerator gen
            )
        {
            gen.Emit(OpCodes.Ldarg_0);
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stfld, field);
            }
            gen.Emit(OpCodes.Ret);
        }
        #endregion

        #region CreateFieldGetData
        private static object syncFieldGetDataRoot = new object();
        private static Dictionary<FieldInfo, DynamicMethod> fieldGetDatas = new Dictionary<FieldInfo, DynamicMethod>();

        public static T CreateGetData<T>(FieldInfo field)
        {
            return (T)(Object)CreateGetData(field).CreateDelegate(typeof(T));
        }

        public static T CreateGetData<T>(FieldInfo field, object target)
        {
            return (T)(Object)CreateGetData(field).CreateDelegate(typeof(T), target);
        }

        public static DynamicMethod CreateGetData(FieldInfo field)
        {
            if (field == null)
                throw new ArgumentNullException("field");
            DynamicMethod dynamicMethod;
            lock (syncFieldGetDataRoot)
            {
                if (fieldGetDatas.TryGetValue(field, out dynamicMethod))
                    return dynamicMethod;

                string name = "Get" + field.MetadataToken.ToString();
                Module module = field.DeclaringType.Module;
                Type returnType = field.FieldType;
                Type[] parameterTypes = (field.IsStatic) ? null :
                    new Type[] { field.DeclaringType };

                dynamicMethod = new DynamicMethod(
                    name,
                    returnType,
                    parameterTypes,
                    module);
                GenerateFieldGetDataBody(
                    field,
                    dynamicMethod.GetILGenerator());
                fieldGetDatas.Add(field, dynamicMethod);
                return dynamicMethod;
            }

        }

        private static void GenerateFieldGetDataBody(
            FieldInfo field,
            ILGenerator gen
            )
        {
            if (!field.IsStatic)
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldsfld, field);
            }
            gen.Emit(OpCodes.Ret);
        }

        #endregion

        #region CreateInvoke
        private static object syncMethodRoot = new object();
        private static Dictionary<MethodInfo, DynamicMethod> methods = new Dictionary<MethodInfo, DynamicMethod>();

        public static T CreateInvoke<T>(MethodInfo method)
        {
            return (T)(object)CreateInvoke(method).CreateDelegate(typeof(T));
        }

        public static T CreateInvoke<T>(MethodInfo method, object target)
        {
            return (T)(object)CreateInvoke(method).CreateDelegate(typeof(T), target);
        }

        public static DynamicMethod CreateInvoke(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            DynamicMethod dynamicMethod;
            lock (syncMethodRoot)
            {
                if (methods.TryGetValue(method, out dynamicMethod))
                    return dynamicMethod;
                string name = method.Name+method.MetadataToken.ToString();
                Module module = method.DeclaringType.Module;
                Type returnType = method.ReturnType;
                Type[] parameterTypes = GetParameterTypes(method);

                dynamicMethod = new DynamicMethod(
                    name,
                    returnType,
                    parameterTypes,
                    module);
                GenerateInvokeBody(method, dynamicMethod.GetILGenerator(), parameterTypes);
                methods.Add(method, dynamicMethod);
                return dynamicMethod;
            }
        }

        private static void GenerateInvokeBody(
            MethodInfo method,
            ILGenerator gen,
            Type[] parameterTypes
            )
        {
            for (int i = 0; i < parameterTypes.Length; ++i)
                gen.Emit(OpCodes.Ldarg, i);

            OpCode callCode;
            if (method.IsVirtual)
                callCode = OpCodes.Callvirt;
            else
                callCode = OpCodes.Call;

            gen.EmitCall(callCode, method, null);

            gen.Emit(OpCodes.Ret);
        }

        private static Type[] GetParameterTypes(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            Type[] types;
            if (method.IsStatic)
            {
                types = new Type[parameters.Length];
                for (int i = 0; i < parameters.Length; ++i)
                    types[i] = parameters[i].ParameterType;
            }
            else
            {
                types = new Type[parameters.Length + 1];
                types[0] = method.DeclaringType;
                for (int i = 0; i < parameters.Length; ++i)
                    types[i + 1] = parameters[i].ParameterType;
            }

            return types;
        }
        #endregion
    }
}
