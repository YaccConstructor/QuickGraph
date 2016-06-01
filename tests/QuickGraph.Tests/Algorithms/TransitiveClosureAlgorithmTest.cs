using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class TransitiveClosureAlgorithmTest
    {

        [TestMethod]
        public void SmallTest()
        {
            var graph = new BidirectionalGraph<int, Edge<int>>();
            graph.AddVerticesAndEdgeRange(new[] { new Edge<int>(1, 2), new Edge<int>(2, 3)});

            var result = graph.ComputeTransitiveClosure();
            Assert.AreEqual(3, result.EdgeCount);
        }

        [TestMethod]
        public void Test()
        {
            var graph = new BidirectionalGraph<int, Edge<int>>();
            graph.AddVerticesAndEdgeRange(new[] { new Edge<int>(1, 2) , new Edge<int>(2, 3), new Edge<int>(3, 4), new Edge<int>(3, 5) });

            var result = graph.ComputeTransitiveClosure();
            Assert.AreEqual(9, result.EdgeCount);
        }
    }
}
