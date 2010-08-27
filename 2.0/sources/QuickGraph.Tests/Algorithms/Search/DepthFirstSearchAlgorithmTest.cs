using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.Search
{
    [TestFixture]
    public class DepthFirstAlgorithmSearchTest
    {
        private Dictionary<string,string> parents;
        private Dictionary<string,int> discoverTimes;
        private Dictionary<string,int> finishTimes;
        private int time;
        private AdjacencyGraph<string,Edge<string>> g;
        private DepthFirstSearchAlgorithm<string,Edge<string>> dfs;

        private void StartVertex(Object sender, VertexEventArgs<string> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.White);
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<string> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.Gray);
            Assert.AreEqual(dfs.VertexColors[parents[args.Vertex]], GraphColor.Gray);

            discoverTimes[args.Vertex] = time++;
        }

        private void ExamineEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Source], GraphColor.Gray);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.White);
            parents[args.Edge.Target] = args.Edge.Source;
        }

        private void BackEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.Gray);
        }

        private void FowardOrCrossEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Edge.Target], GraphColor.Black);
        }

        private void FinishVertex(Object sender, VertexEventArgs<string> args)
        {
            Assert.AreEqual(dfs.VertexColors[args.Vertex], GraphColor.Black);
            finishTimes[args.Vertex] = time++;
        }

        public bool IsDescendant(string u, string v)
        {
            string t = null;
            string p = u;
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

        [SetUp]
        public void Init()
        {

            parents = new Dictionary<string,string>();
            discoverTimes = new Dictionary<string,int>();
            finishTimes = new Dictionary<string,int>();
            time = 0;
            g = new AdjacencyGraph<string,Edge<string>>(true);
            dfs = new DepthFirstSearchAlgorithm<string, Edge<string>>(g);

            dfs.StartVertex += new VertexEventHandler<string>(this.StartVertex);
            dfs.DiscoverVertex += new VertexEventHandler<string>(this.DiscoverVertex);
            dfs.ExamineEdge += new EdgeEventHandler<string, Edge<string>>(this.ExamineEdge);
            dfs.TreeEdge += new EdgeEventHandler<string, Edge<string>>(this.TreeEdge);
            dfs.BackEdge += new EdgeEventHandler<string, Edge<string>>(this.BackEdge);
            dfs.ForwardOrCrossEdge += new EdgeEventHandler<string, Edge<string>>(this.FowardOrCrossEdge);
            dfs.FinishVertex += new VertexEventHandler<string>(this.FinishVertex);
        }

        [TearDown]
        public void TearDown()
        {
            dfs.StartVertex -= new VertexEventHandler<string>(this.StartVertex);
            dfs.DiscoverVertex -= new VertexEventHandler<string>(this.DiscoverVertex);
            dfs.ExamineEdge -= new EdgeEventHandler<string, Edge<string>>(this.ExamineEdge);
            dfs.TreeEdge -= new EdgeEventHandler<string, Edge<string>>(this.TreeEdge);
            dfs.BackEdge -= new EdgeEventHandler<string, Edge<string>>(this.BackEdge);
            dfs.ForwardOrCrossEdge -= new EdgeEventHandler<string, Edge<string>>(this.FowardOrCrossEdge);
            dfs.FinishVertex -= new VertexEventHandler<string>(this.FinishVertex);
        }

        [Test]
        public void GraphWithSelfEdges()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraph<string, Edge<string>>(false);
            g.AddVertex("v1");
            g.AddEdge(new Edge<string>("v1","v1"));

            foreach (string v in g.Vertices)
                parents[v] = v;

            // compute
            dfs.Compute();

            CheckDfs();
        }

        [Test]
        public void GraphWithoutSelfEdges()
        {
            AdjacencyGraph<string, Edge<string>> g = new AdjacencyGraphFactory().FileDependency();

            foreach (string v in g.Vertices)
                parents[v] = v;

            // compute
            dfs.Compute();
            CheckDfs();
        }

        protected void CheckDfs()
        {
            // check
            // all vertices should be black
            foreach (string v in g.Vertices)
            {
                Assert.IsTrue(dfs.VertexColors.ContainsKey(v));
                Assert.AreEqual(dfs.VertexColors[v], GraphColor.Black);
            }

            foreach (string u in g.Vertices)
            {
                foreach (string v in g.Vertices)
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
