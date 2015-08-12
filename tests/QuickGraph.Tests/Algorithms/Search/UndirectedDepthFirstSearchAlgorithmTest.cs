using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using Microsoft.Pex.Framework;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public partial class UndirectedDepthFirstAlgorithmSearchTest
    {
        private static bool IsDescendant<TVertex>(
            Dictionary<TVertex,TVertex> parents,
            TVertex u, 
            TVertex v)
        {
            TVertex t;
            TVertex p = u;
            do
            {
                t = p;
                p = parents[t];
                if (p.Equals(v))
                    return true;
            }
            while (!t.Equals(p));

            return false;
        }

        [TestMethod]
        public void UndirectedDepthFirstSearchAll()
        {
            Parallel.ForEach(TestGraphFactory.GetUndirectedGraphs(), g =>
                this.UndirectedDepthFirstSearch(g));
        }

        [PexMethod]
        public void UndirectedDepthFirstSearch<TVertex,TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var parents = new Dictionary<TVertex, TVertex>();
            var discoverTimes = new Dictionary<TVertex, int>();
            var finishTimes = new Dictionary<TVertex, int>();
            int time = 0;
            var dfs = new UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge>(g);

            dfs.StartVertex += args =>
            {
                Assert.AreEqual(dfs.VertexColors[args], GraphColor.White);
                Assert.IsFalse(parents.ContainsKey(args));
                parents[args] = args;
            };

            dfs.DiscoverVertex += args =>
            {
                Assert.AreEqual(dfs.VertexColors[args], GraphColor.Gray);
                Assert.AreEqual(dfs.VertexColors[parents[args]], GraphColor.Gray);

                discoverTimes[args] = time++;
            };

            dfs.ExamineEdge += (sender, args) =>
            {
                Assert.AreEqual(dfs.VertexColors[args.Source], GraphColor.Gray);
            };

            dfs.TreeEdge += (sender, args) =>
            {
                var source = args.Source;
                var target = args.Target;
                Assert.AreEqual(dfs.VertexColors[target], GraphColor.White);
                parents[target] = source;
            };

            dfs.BackEdge += (sender, args) =>
            {
                Assert.AreEqual(dfs.VertexColors[args.Target], GraphColor.Gray);
            };

            dfs.ForwardOrCrossEdge += (sender, args) =>
            {
                Assert.AreEqual(dfs.VertexColors[args.Target], GraphColor.Black);
            };

            dfs.FinishVertex += args =>
            {
                Assert.AreEqual(dfs.VertexColors[args], GraphColor.Black);
                finishTimes[args] = time++;
            };

            dfs.Compute();

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
                    if (!u.Equals(v))
                    {
                        Assert.IsTrue(
                            finishTimes[u] < discoverTimes[v]
                            || finishTimes[v] < discoverTimes[u]
                            || (
                            discoverTimes[v] < discoverTimes[u]
                            && finishTimes[u] < finishTimes[v]
                            && IsDescendant(parents, u, v)
                            )
                            || (
                            discoverTimes[u] < discoverTimes[v]
                            && finishTimes[v] < finishTimes[u]
                            && IsDescendant(parents, v, u)
                            )
                            );
                    }
                }
            }
        }
    }
}
