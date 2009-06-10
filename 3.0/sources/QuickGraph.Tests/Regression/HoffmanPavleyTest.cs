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
            Console.WriteLine("path: {0}", test1.ComputedShortestPathCount);
            foreach (var path in test1.ComputedShortestPaths)
            {
                foreach(var edge in path)
                    Console.Write(edge + ":");
                Console.WriteLine();
            }
        }
    }
}
