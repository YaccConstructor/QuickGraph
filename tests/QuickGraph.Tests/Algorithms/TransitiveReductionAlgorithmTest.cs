using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class TransitiveReductionAlgorithmTest
    {
        [TestMethod]
        public void SmallTest()
        {
            var graph = new BidirectionalGraph<int, Edge<int>>();

            graph.AddVerticesAndEdgeRange(new[]
            {
                new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(1, 4),
                new Edge<int>(1, 5), new Edge<int>(2, 4), new Edge<int>(3, 4),
                new Edge<int>(3, 5), new Edge<int>(4, 5)
            });

            var result = graph.ComputeTransitiveReduction();
            Assert.AreEqual(5, result.EdgeCount);
        }

        [TestMethod]
        public void Test()
        {
            var graph = new BidirectionalGraph<int, Edge<int>>();

            graph.AddVerticesAndEdgeRange(new[]
            {
                new Edge<int>(0, 1), new Edge<int>(0, 2), new Edge<int>(0, 3),
                new Edge<int>(2, 3), new Edge<int>(2, 4), new Edge<int>(2, 5),
                new Edge<int>(3, 5), new Edge<int>(4, 5), new Edge<int>(6, 5),
                new Edge<int>(6, 7), new Edge<int>(7, 4)
            });

            var result = graph.ComputeTransitiveReduction();
            Assert.AreEqual(8, result.EdgeCount);
        }

    }
}
