using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.RankedShortestPath;

namespace QuickGraph.Tests.Regression
{
    [TestClass]
    public partial class HoffmanPavleyTest
    {
        [TestMethod]
        [WorkItem(13526)]
        public void NotEnoughPaths()
        {
            int ii = 0;
            var graph = new BidirectionalGraph<int, TaggedEdge<int, int>>();
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(493, 495, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(495, 493, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(497, 499, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(499, 497, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(499, 501, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(501, 499, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(501, 503, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(503, 501, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(503, 505, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(505, 503, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(505, 507, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(507, 505, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(507, 509, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(509, 507, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(509, 511, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(511, 509, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2747, 2749, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2749, 2747, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2749, 2751, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2751, 2749, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2751, 2753, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2753, 2751, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2753, 2755, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2755, 2753, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2755, 2757, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2757, 2755, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2757, 2759, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2759, 2757, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2761, 2763, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2763, 2761, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2765, 2767, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2767, 2765, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2763, 2765, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2765, 2763, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(654, 978, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(978, 654, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(978, 1302, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1302, 978, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1302, 1626, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1626, 1302, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1626, 1950, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1950, 1626, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1950, 2274, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2274, 1950, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2274, 2598, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2598, 2274, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(513, 676, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(676, 513, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2767, 2608, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2608, 2767, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2287, 2608, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2608, 2287, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(676, 999, ii++)); ;
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(999, 676, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1321, 1643, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1643, 1321, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1643, 1965, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1965, 1643, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1965, 2287, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2287, 1965, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(999, 1321, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1321, 999, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2745, 2747, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2747, 2745, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(650, 491, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(491, 650, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(650, 970, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(970, 650, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2258, 2582, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2582, 2258, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(970, 1291, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1291, 970, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1935, 2258, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2258, 1935, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1291, 1613, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1613, 1291, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1613, 1935, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(1935, 1613, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2582, 2745, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2745, 2582, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(495, 497, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(497, 495, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(511, 513, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(513, 511, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(491, 493, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(493, 491, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(491, 654, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(654, 491, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2761, 2598, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2598, 2761, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2761, 2759, ii++));
            graph.AddVerticesAndEdge(new TaggedEdge<int, int>(2759, 2761, ii++));

            var test1 = new HoffmanPavleyRankedShortestPathAlgorithm<int, TaggedEdge<int, int>>(graph, e => 1.0);
            test1.ShortestPathCount = 5;
            test1.Compute(1626, 1965);
            Assert.AreEqual(4, test1.ComputedShortestPathCount);
            TestConsole.WriteLine("path: {0}", test1.ComputedShortestPathCount);
            foreach (var path in test1.ComputedShortestPaths)
            {
                foreach (var edge in path)
                    Console.Write(edge + ":");
                TestConsole.WriteLine();
            }
        }

        [TestMethod]
        [WorkItem(13111)]
        public void InfiniteLoop13111()
        {
            int ii = 0;
            var mvGraph2 = new BidirectionalGraph<int, TaggedEdge<int, int>>();

            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(0, 1, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(1, 2, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(2, 3, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(3, 4, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(4, 5, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(5, 0, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(1, 5, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(5, 1, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(2, 5, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(1, 0, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(2, 1, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(3, 2, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(4, 3, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(5, 4, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(0, 5, ii++));
            mvGraph2.AddVerticesAndEdge(new TaggedEdge<int, int>(5, 2, ii++));

            var test1 = new HoffmanPavleyRankedShortestPathAlgorithm<int, TaggedEdge<int, int>>(mvGraph2, E => 1.0);
            test1.ShortestPathCount = 5;
            test1.Compute(5, 2);
            TestConsole.WriteLine("path: {0}", test1.ComputedShortestPathCount);
            foreach (var path in test1.ComputedShortestPaths)
            {
                foreach(var edge in path)
                    Console.Write(edge + ":");
                TestConsole.WriteLine();
            }
        }
    }
}
