using System;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [TypeFixture(typeof(IMutableVertexAndEdgeListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public class AllVerticesGraphAugmentorAlgorithmTest
    {
        private AllVerticesGraphAugmentorAlgorithm<string, Edge<string>> augmentor;

        public void SetUp(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.augmentor = new AllVerticesGraphAugmentorAlgorithm<string, Edge<string>>(
                g,
                new StringVertexFactory(),
                new EdgeFactory<string>()
                );
        }

        [TearDown]
        public void TearDown()
        {
            if (this.augmentor != null)
            {
                this.augmentor.Rollback();
                this.augmentor = null;
            }
        }

        [Test]
        public void AddSuperSourceAndSink(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            SetUp(g);
            int vertexCount = g.VertexCount;
            this.augmentor.Compute();

            Assert.AreEqual(vertexCount + 2, g.VertexCount);
            Assert.IsTrue(g.ContainsVertex(this.augmentor.SuperSource));
            Assert.IsTrue(g.ContainsVertex(this.augmentor.SuperSink));
        }

        [Test]
        public void AddAndRemove(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            SetUp(g);
            int vertexCount = g.VertexCount;
            int edgeCount = g.EdgeCount;
            this.augmentor.Compute();
            this.augmentor.Rollback();
            Assert.AreEqual(g.VertexCount, vertexCount);
            Assert.AreEqual(g.EdgeCount, edgeCount);
        }

        [Test]
        public void AddAndVerifySourceConnected(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            SetUp(g);
            this.augmentor.Compute();
            foreach (string v in g.Vertices)
            {
                if (v == this.augmentor.SuperSource)
                    continue;
                if (v == this.augmentor.SuperSink)
                    continue;
                Assert.IsTrue(g.ContainsEdge(this.augmentor.SuperSource, v));
            }
        }

        [Test]
        public void AddAndVerifySinkConnected(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            SetUp(g);
            this.augmentor.Compute();
            foreach (string v in g.Vertices)
            {
                if (v == this.augmentor.SuperSink)
                    continue;
                if (v == this.augmentor.SuperSink)
                    continue;
                Assert.IsTrue(g.ContainsEdge(v, this.augmentor.SuperSink));
            }
        }

        private sealed class StringVertexFactory : IVertexFactory<string>
        {
            private int id = 0;

            public string CreateVertex()
            {
                return "Super"+(++id).ToString();
            }
        }
    }
}
