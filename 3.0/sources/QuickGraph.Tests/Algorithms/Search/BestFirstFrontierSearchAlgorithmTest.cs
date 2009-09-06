using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Algorithms.Search
{
    [TestClass, PexClass(typeof(BestFirstFrontierSearchAlgorithm<,>))]
    public partial class BestFirstFrontierSearchAlgorithmTest
    {
        [TestMethod]
        public void KrokFFig2Example()
        {
            var g = new BidirectionalGraph<char, SEquatableEdge<char>>();
            g.AddVerticesAndEdge(new SEquatableEdge<char>('A', 'C'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('A', 'B'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('B', 'E'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('B', 'D'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('E', 'F'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('E', 'G'));

            RunSearch(g);
        }

        [TestMethod]
        public void BestFirstFrontierSearchAllGraphs()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
                RunSearch(g);
        }

        [PexMethod]
        public void RunSearch<TVertex, TEdge>(
            [PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> g)
            where TEdge: IEdge<TVertex>
        {
            if (g.VertexCount == 0) return;

            Func<TEdge, double> edgeWeights = e => 1;
            var distanceRelaxer = ShortestDistanceRelaxer.Instance;

            var search = new BestFirstFrontierSearchAlgorithm<TVertex, TEdge>(
                null, 
                g, 
                edgeWeights, 
                distanceRelaxer);
            var root = Enumerable.First(g.Vertices);
            var target = Enumerable.Last(g.Vertices);

            search.Compute(root, target);
        }
    }
}
