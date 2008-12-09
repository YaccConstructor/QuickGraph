using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public class DepthFirstAlgorithmSearchTest
    {
        private Dictionary<IdentifiableVertex, IdentifiableVertex> parents;
        private Dictionary<IdentifiableVertex, int> discoverTimes;
        private Dictionary<IdentifiableVertex, int> finishTimes;
        private int time;
        private AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> g;
        private DepthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> dfs;

        private void StartVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.White);
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.Gray);
            Assert.AreEqual(dfs.VertexColors[parents[args.Vertex]], GraphColor.Gray);

            discoverTimes[args.Vertex] = time++;
        }

        private void ExamineEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Source], GraphColor.Gray);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.White);
            parents[args.Edge.Target] = args.Edge.Source;
        }

        private void BackEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.Gray);
        }

        private void FowardOrCrossEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.Black);
        }

        private void FinishVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.Black);
            finishTimes[args.Vertex] = time++;
        }

        public bool IsDescendant(IdentifiableVertex u, IdentifiableVertex v)
        {
            IdentifiableVertex t = null;
            IdentifiableVertex p = u;
            do
            {
                t = p;
                p = parents[t];
                if (p == v)
                    return true;
            }
            while (t != p);

            return false;
        }

        public void Init()
        {
            parents = new Dictionary<IdentifiableVertex, IdentifiableVertex>();
            discoverTimes = new Dictionary<IdentifiableVertex, int>();
            finishTimes = new Dictionary<IdentifiableVertex, int>();
            time = 0;
            g = new AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(true);
            dfs = new DepthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(g);

            dfs.StartVertex += new VertexEventHandler<IdentifiableVertex>(this.StartVertex);
            dfs.DiscoverVertex += new VertexEventHandler<IdentifiableVertex>(this.DiscoverVertex);
            dfs.ExamineEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.ExamineEdge);
            dfs.TreeEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.TreeEdge);
            dfs.BackEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.BackEdge);
            dfs.ForwardOrCrossEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.FowardOrCrossEdge);
            dfs.FinishVertex += new VertexEventHandler<IdentifiableVertex>(this.FinishVertex);
        }

        public void TearDown()
        {
            dfs.StartVertex -= new VertexEventHandler<IdentifiableVertex>(this.StartVertex);
            dfs.DiscoverVertex -= new VertexEventHandler<IdentifiableVertex>(this.DiscoverVertex);
            dfs.ExamineEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.ExamineEdge);
            dfs.TreeEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.TreeEdge);
            dfs.BackEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.BackEdge);
            dfs.ForwardOrCrossEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.FowardOrCrossEdge);
            dfs.FinishVertex -= new VertexEventHandler<IdentifiableVertex>(this.FinishVertex);
        }

        [TestMethod]
        public void DepthFirstSearchAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
            {
                this.Init();
                try
                {
                    foreach (var v in g.Vertices)
                        parents[v] = v;
                    dfs.Compute();
                    CheckDfs();
                }
                finally
                {
                    this.TearDown();
                }
            }
        }

        protected void CheckDfs()
        {
            // check
            // all vertices should be black
            foreach (var v in g.Vertices)
            {
                Assert.IsTrue(dfs.VertexColors.ContainsKey(v));
                Assert.AreEqual(dfs.VertexColors[v], GraphColor.Black);
            }

            foreach (var u in g.Vertices)
            {
                foreach (var v in g.Vertices)
                {
                    if (u != v)
                    {
                        Assert.IsTrue(
                            finishTimes[u] < discoverTimes[v]
                            || finishTimes[v] < discoverTimes[u]
                            || (
                            discoverTimes[v] < discoverTimes[u]
                            && finishTimes[u] < finishTimes[v]
                            && IsDescendant(u, v)
                            )
                            || (
                            discoverTimes[u] < discoverTimes[v]
                            && finishTimes[v] < finishTimes[u]
                            && IsDescendant(v, u)
                            )
                            );
                    }
                }
            }
        }
    }
}
