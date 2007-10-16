using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public partial class FactoryCompilerTest
    {
        public interface IFoo<T>
        {
            T Boo();
        }
        public class Foo : IFoo<int>
        {
            public int Boo()
            {
                return 0;
            }
        }

        [Test, PexMethod]
        public void CreateVertexFactory()
        {
            MethodInfo boo = typeof(Foo).GetMethod("Boo");

            IVertexFactory<Foo> factory = FactoryCompiler.GetVertexFactory<Foo>();
            Assert.IsNotNull(factory);
            Foo foo  = factory.CreateVertex();
            Assert.IsNotNull(foo);
        }

        [Test, PexMethod]
        public void CreateVertexFactoryTwice()
        {
            IVertexFactory<object> factory = FactoryCompiler.GetVertexFactory<object>();
            Assert.IsNotNull(factory);
            Assert.IsNotNull(factory.CreateVertex());
            IVertexFactory<object> factory2 = FactoryCompiler.GetVertexFactory<object>();
            Assert.IsNotNull(factory2);
            Assert.IsNotNull(factory2.CreateVertex());
        }

        [Test, PexMethod]
        public void CreateEdgeFactory()
        {
            IEdgeFactory<Foo,Edge<Foo>> factory = FactoryCompiler.GetEdgeFactory<Foo,Edge<Foo>>();
            Assert.IsNotNull(factory);
            Foo source = new Foo();
            Foo target = new Foo();
            Edge<Foo> edge = factory.CreateEdge(source, target);

            Assert.IsNotNull(edge);
            Assert.IsNotNull(edge.Source);
            Assert.IsNotNull(edge.Target);
        }

        [Test, PexMethod]
        public void CreateEdgeFactoryTwice()
        {
            CreateEdgeFactory();
            CreateEdgeFactory();
        }

        [Test, PexMethod]
        public void CreateDifferentFactories()
        {
            FactoryCompiler.GetEdgeFactory<int, Edge<int>>();
            FactoryCompiler.GetEdgeFactory<object, Edge<object>>();
            FactoryCompiler.GetEdgeFactory<string, Edge<string>>();
            FactoryCompiler.GetEdgeFactory<long, Edge<long>>();
            FactoryCompiler.GetEdgeFactory<float, Edge<float>>();
            FactoryCompiler.GetEdgeFactory<Foo, Edge<Foo>>();
        }
    }
}
