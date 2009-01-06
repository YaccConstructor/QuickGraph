using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Collections;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;

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

                var pairs = new List<VertexPair<IdentifiableVertex>>();
                foreach(var v in g.Vertices)
                    foreach(var w in g.Vertices)
                        if (!v.Equals(w))
                        pairs.Add(new VertexPair<IdentifiableVertex>(v,w));

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
            TVertex ancestor;
            foreach(var pair in pairs)
                if (lca(pair, out ancestor))
                { }
                    //Console.WriteLine("{0}-{1} -> {2}", pair.Source, pair.Target, ancestor);
        }
    }
}
