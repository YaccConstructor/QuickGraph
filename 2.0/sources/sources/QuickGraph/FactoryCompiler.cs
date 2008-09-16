using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace QuickGraph
{
    public static class FactoryCompiler
    {
        private static readonly object syncRoot = new object();
        private static AssemblyBuilder assembly;
        private static ModuleBuilder module;

        private static void CreateAssembly()
        {
            if (assembly == null)
            {
                assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                    new AssemblyName("QuickGraph.FactoryCompilers"),
                    AssemblyBuilderAccess.Run
                    );
                module = assembly.DefineDynamicModule("QuickGraph.FactoryCompilers");
            }
        }

        private static string GetVertexFactoryName(Type factoredType)
        {
            return string.Format(
                "{0}VertexFactory", factoredType.MetadataToken
                );
        }

        private static string GetEdgeFactoryName(Type vertexType, Type edgeType)
        {
            return string.Format(
                "{0}{1}EdgeFactory", vertexType.MetadataToken, edgeType.MetadataToken);
        }

        public static IVertexFactory<TVertex> GetVertexFactory<TVertex>()
        {
            return (IVertexFactory<TVertex>)Activator.CreateInstance(GetVertexFactoryType<TVertex>());
        }

        private static Type GetVertexFactoryType<TVertex>()
        {
            lock (syncRoot)
            {
                CreateAssembly();
                Type factoryType = assembly.GetType(GetVertexFactoryName(typeof(TVertex)), false);
                if (factoryType != null)
                    return factoryType;

                ConstructorInfo constructor =
                    typeof(TVertex).GetConstructor(new Type[] { });
                if (constructor == null)
                    throw new ArgumentException(String.Format("Type {0} does not have public construtor", typeof(TVertex)));

                TypeBuilder type = module.DefineType(GetVertexFactoryName(typeof(TVertex)),
                    TypeAttributes.Sealed | TypeAttributes.Public);
                type.AddInterfaceImplementation(typeof(IVertexFactory<TVertex>));


                // CreateVertex method
                MethodBuilder createVertex = type.DefineMethod(
                    "CreateVertex",
                    MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask,
                    typeof(TVertex),
                    Type.EmptyTypes
                    );
                ILGenerator gen = createVertex.GetILGenerator();
                gen.Emit(OpCodes.Newobj, constructor);
                gen.Emit(OpCodes.Ret);
                factoryType = type.CreateType();
                return factoryType;
            }
        }

        public static IEdgeFactory<TVertex, TEdge> GetEdgeFactory<TVertex, TEdge>()
            where TEdge : IEdge<TVertex>
        {
            return (IEdgeFactory<TVertex, TEdge>)Activator.CreateInstance(GetEdgeFactoryType<TVertex, TEdge>());
        }

        private static Type GetEdgeFactoryType<TVertex, TEdge>()
            where TEdge : IEdge<TVertex>
        {
            lock (syncRoot)
            {
                CreateAssembly();
                string typeName = GetEdgeFactoryName(typeof(TVertex), typeof(TEdge));
                Type factoryType = assembly.GetType(typeName, false);
                if (factoryType != null)
                    return factoryType;

                ConstructorInfo constructor =
                    typeof(TEdge).GetConstructor(new Type[] { typeof(TVertex), typeof(TVertex) });
                if (constructor == null)
                    throw new ArgumentException(String.Format("Type {0} does not have a construtor that takes 2 Vertex", typeof(TVertex)));

                TypeBuilder type = module.DefineType(typeName,
                    TypeAttributes.Sealed | TypeAttributes.Public);
                type.AddInterfaceImplementation(typeof(IEdgeFactory<TVertex, TEdge>));

                // CreateVertex method
                MethodBuilder createEdge = type.DefineMethod(
                    "CreateEdge",
                    MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask,
                    typeof(TEdge),
                    new Type[] { typeof(TVertex), typeof(TVertex) }
                    );
                ILGenerator gen = createEdge.GetILGenerator();
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Ldarg_2);
                gen.Emit(OpCodes.Newobj, constructor);
                gen.Emit(OpCodes.Ret);
                factoryType = type.CreateType();
                return factoryType;
            }
        }
    }
}
