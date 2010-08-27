using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class StronglyConnectedComponentAlgorithmTest
    {
        [Test, PexMethod]
        public void EmptyGraph()
        {
            IVertexListGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(true);
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(0, strong.ComponentCount);
            checkStrong(strong);
        }

        [Test, PexMethod]
        public void OneVertex()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(true);
            g.AddVertex("test");
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(1, strong.ComponentCount);

            checkStrong(strong);
        }

        [Test, PexMethod]
        public void TwoVertex()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(true);
            g.AddVertex("v1");
            g.AddVertex("v2");
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(2, strong.ComponentCount);

            checkStrong(strong);
        }

        [Test, PexMethod]
        public void TwoVertexOnEdge()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(true);
            g.AddVertex("v1");
            g.AddVertex("v2");
            g.AddEdge(new Edge<string>("v1", "v2"));
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(2, strong.ComponentCount);

            checkStrong(strong);
        }


        [Test, PexMethod]
        public void TwoVertexCycle()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(true);
            g.AddVertex("v1");
            g.AddVertex("v2");
            g.AddEdge(new Edge<string>("v1", "v2"));
            g.AddEdge(new Edge<string>("v2", "v1"));
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(1, strong.ComponentCount);

            checkStrong(strong);
        }

        [Test, PexMethod]
        public void Loop()
        {
            IVertexListGraph<string,Edge<string>> g = new AdjacencyGraphFactory().Loop();
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);
            strong.Compute();
            Assert.AreEqual(1, strong.ComponentCount);

            checkStrong(strong);
        }

        [Test, PexMethod]
        public void Simple()
        {
            IVertexListGraph<string, Edge<string>> g = new AdjacencyGraphFactory().Simple();
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);

            strong.Compute();
            checkStrong(strong);
        }

        [Test, PexMethod]
        public void FileDependency()
        {
            IVertexListGraph<string, Edge<string>> g = new AdjacencyGraphFactory().FileDependency();
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);

            strong.Compute();
            checkStrong(strong);
        }

        [Test, PexMethod]
        public void RegularLattice()
        {
            IVertexListGraph<string, Edge<string>> g = new AdjacencyGraphFactory().RegularLattice10x10();
            StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong = new StronglyConnectedComponentsAlgorithm<string, Edge<String>>(g);

            strong.Compute();
            checkStrong(strong);
        }

        private void checkStrong(StronglyConnectedComponentsAlgorithm<string, Edge<String>> strong)
        {
            Assert.AreEqual(strong.VisitedGraph.VertexCount, strong.Components.Count);
            Assert.AreEqual(strong.VisitedGraph.VertexCount, strong.DiscoverTimes.Count);
            Assert.AreEqual(strong.VisitedGraph.VertexCount, strong.Roots.Count);

            foreach (string v in strong.VisitedGraph.Vertices)
            {
                Assert.IsTrue(strong.Components.ContainsKey(v));
                Assert.IsTrue(strong.DiscoverTimes.ContainsKey(v));
            }

            foreach (KeyValuePair<string,int> de in strong.Components)
            {
                Assert.IsNotNull(de.Key);
                Assert.LowerEqualThan(de.Value, strong.ComponentCount);
            }

            foreach (KeyValuePair<string,int> de in strong.DiscoverTimes)
            {
                Assert.IsNotNull(de.Key);
            }
        }
    }
}
