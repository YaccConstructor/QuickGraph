using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Collections;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class TarjanOfflineLeastCommonAncestorAlgorithmTest
    {
        [TestMethod]
        public void TarjanOfflineLeastCommonAncestorAlgorithmAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
            {
                if (g.VertexCount == 0) continue;

                var pairs = new List<VertexPair<string>>();
                foreach(var v in g.Vertices)
                    foreach(var w in g.Vertices)
                        if (!v.Equals(w))
                            pairs.Add(new VertexPair<string>(v,w));

                int count = 0;
                foreach (var root in g.Vertices)
                {
                    this.TarjanOfflineLeastCommonAncestorAlgorithm(
                        g,
                        root,
                        pairs.ToArray());
                    if (count++ > 10) break;
                }
            }
        }

        [PexMethod]
        public void TarjanOfflineLeastCommonAncestorAlgorithm<TVertex, TEdge>(
            [PexAssumeNotNull]IVertexListGraph<TVertex, TEdge> g,
            [PexAssumeNotNull]TVertex root,
            [PexAssumeNotNull]VertexPair<TVertex>[] pairs
            )
            where TEdge : IEdge<TVertex>
        {
            var lca = g.OfflineLeastCommonAncestorTarjan(root, pairs);
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(g);
            using(ObserverScope.Create(dfs, predecessors))
                dfs.Compute(root);

            TVertex ancestor;
            foreach(var pair in pairs)
                if (lca(pair, out ancestor))
                {
                    Assert.IsTrue(EdgeExtensions.IsPredecessor(predecessors.VertexPredecessors, root, pair.Source));
                    Assert.IsTrue(EdgeExtensions.IsPredecessor(predecessors.VertexPredecessors, root, pair.Target));
                }
        }
    }
}
