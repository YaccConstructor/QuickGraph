using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms.Search
{
    [TestClass, PexClass(typeof(BestFirstFrontierSearchAlgorithm<,>))]
    public partial class BestFirstFrontierSearchAlgorithmTest
    {
        [PexMethod]
        public void RunSearch<TVertex, TEdge>(
            [PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> g)
            where TEdge: IEdge<TVertex>, IEquatable<TEdge>
        {
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
