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

        public static IVertexFactory<Vertex> GetVertexFactory<Vertex>()
        {
            return (IVertexFactory<Vertex>)Activator.CreateInstance(GetVertexFactoryType<Vertex>());
        }

        private static Type GetVertexFactoryType<Vertex>()
        {
            lock (syncRoot)
            {
                CreateAssembly();
                Type factoryType = assembly.GetType(GetVertexFactoryName(typeof(Vertex)), false);
                if (factoryType != null)
                    return factoryType;

                ConstructorInfo constructor =
                    typeof(Vertex).GetConstructor(new Type[] { });
                if (constructor == null)
                    throw new ArgumentException(String.Format("Type {0} does not have public construtor", typeof(Vertex)));

                TypeBuilder type = module.DefineType(GetVertexFactoryName(typeof(Vertex)),
                    TypeAttributes.Sealed | TypeAttributes.Public);
                type.AddInterfaceImplementation(typeof(IVertexFactory<Vertex>));


                // CreateVertex method
                MethodBuilder createVertex = type.DefineMethod(
                    "CreateVertex",
                    MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask,
                    typeof(Vertex),
                    Type.EmptyTypes
                    );
                ILGenerator gen = createVertex.GetILGenerator();
                gen.Emit(OpCodes.Newobj, constructor);
                gen.Emit(OpCodes.Ret);
                factoryType = type.CreateType();
                return factoryType;
            }
        }

        public static IEdgeFactory<Vertex, Edge> GetEdgeFactory<Vertex, Edge>()
            where Edge : IEdge<Vertex>
        {
            return (IEdgeFactory<Vertex, Edge>)Activator.CreateInstance(GetEdgeFactoryType<Vertex, Edge>());
        }

        private static Type GetEdgeFactoryType<Vertex, Edge>()
            where Edge : IEdge<Vertex>
        {
            lock (syncRoot)
            {
                CreateAssembly();
                string typeName = GetEdgeFactoryName(typeof(Vertex), typeof(Edge));
                Type factoryType = assembly.GetType(typeName, false);
                if (factoryType != null)
                    return factoryType;

                ConstructorInfo constructor =
                    typeof(Edge).GetConstructor(new Type[] { typeof(Vertex), typeof(Vertex) });
                if (constructor == null)
                    throw new ArgumentException(String.Format("Type {0} does not have a construtor that takes 2 Vertex", typeof(Vertex)));

                TypeBuilder type = module.DefineType(typeName,
                    TypeAttributes.Sealed | TypeAttributes.Public);
                type.AddInterfaceImplementation(typeof(IEdgeFactory<Vertex, Edge>));

                // CreateVertex method
                MethodBuilder createEdge = type.DefineMethod(
                    "CreateEdge",
                    MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask,
                    typeof(Edge),
                    new Type[] { typeof(Vertex), typeof(Vertex) }
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
