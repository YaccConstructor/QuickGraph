using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [TestClass]
    public class AllVerticesGraphAugmentorAlgorithmTest
    {
        [TestMethod]
        public void AugmentAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
                this.Augment(g);
        }

        [PexMethod]
        public void Augment(
            IMutableVertexAndEdgeListGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> g)
        {
            int vertexCount = g.VertexCount;
            int edgeCount = g.EdgeCount;
            int vertexId = g.VertexCount+1;
            int edgeID = g.EdgeCount+1;
            using (var augmentor = new AllVerticesGraphAugmentorAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(
                g,
                () => new IdentifiableVertex((vertexId++).ToString()),
                (s, t) => new IdentifiableEdge<IdentifiableVertex>(s, t, (edgeID++).ToString())
                ))
            {
                VerifyCount(g, augmentor, vertexCount);
                VerifySourceConnector(g, augmentor);
                VerifySinkConnector(g, augmentor);
            }
            Assert.AreEqual(g.VertexCount, vertexCount);
            Assert.AreEqual(g.EdgeCount, edgeCount);
        }

        private static void VerifyCount<TVertex,TEdge>(
            IMutableVertexAndEdgeListGraph<TVertex,TEdge> g, AllVerticesGraphAugmentorAlgorithm<TVertex,TEdge> augmentor, int vertexCount)
            where TEdge : IEdge<TVertex>
        {
            Assert.AreEqual(vertexCount + 2, g.VertexCount);
            Assert.IsTrue(g.ContainsVertex(augmentor.SuperSource));
            Assert.IsTrue(g.ContainsVertex(augmentor.SuperSink));
        }

        private static void VerifySourceConnector<TVertex, TEdge>(IMutableVertexAndEdgeListGraph<TVertex, TEdge> g, AllVerticesGraphAugmentorAlgorithm<TVertex, TEdge> augmentor)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in g.Vertices)
            {
                if (v.Equals(augmentor.SuperSource))
                    continue;
                if (v.Equals(augmentor.SuperSink))
                    continue;
                Assert.IsTrue(g.ContainsEdge(augmentor.SuperSource, v));
            }
        }

        private static void VerifySinkConnector<TVertex, TEdge>(IMutableVertexAndEdgeListGraph<TVertex, TEdge> g, AllVerticesGraphAugmentorAlgorithm<TVertex, TEdge> augmentor)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in g.Vertices)
            {
                if (v.Equals(augmentor.SuperSink))
                    continue;
                if (v.Equals(augmentor.SuperSink))
                    continue;
                Assert.IsTrue(g.ContainsEdge(v, augmentor.SuperSink));
            }
        }

        private sealed class StringVertexFactory 
        {
            private int id = 0;

            public string CreateVertex()
            {
                return "Super"+(++id).ToString();
            }
        }
    }
}
