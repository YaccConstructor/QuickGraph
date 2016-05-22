using System;
using System.Linq;
using System.Management.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.ShortestPath.Yen;

namespace QuickGraph.Tests.Algorithms.ShortestPath
{
    [TestClass]
    public class YenShortestPathsAlgorithmTest
    {

        /*
         * Attempt to use non existing vertices
         */
        [TestMethod]
        public void YenZeroCaseTest()
        {
            var graph = new AdjacencyGraph<char, TaggedEquatableEdge<char, double>>(true);
            var yen = new YenShortestPathsAlgorithm<char>(graph, '1', '5', 10);
            var exeptionWas = false;
            try
            {
                yen.Execute();
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, e is System.Collections.Generic.KeyNotFoundException);
                exeptionWas = true;
            }
            Assert.AreEqual(exeptionWas, true);
        }

        /*
        * Attempt to use for graph that only have one vertex
        * Expecting that Dijkstra’s algorithm couldn't find any ways
        */
        [TestMethod]
        public void YenOneVertexCaseTest()
        {
            var graph = new AdjacencyGraph<char, TaggedEquatableEdge<char, double>>(true);
            graph.AddVertexRange("1");
            var yen = new YenShortestPathsAlgorithm<char>(graph, '1', '1', 10);
            var exeptionWas = false;
            try
            {
                yen.Execute();
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, e is InstanceNotFoundException);
                exeptionWas = true;
            }
            Assert.AreEqual(exeptionWas, true);
        }

        /*
        * Attempt to use for loop graph
        * Expecting that Dijkstra’s algorithm couldn't find any ways
        */
        [TestMethod]
        public void YenLoopCaseTest()
        {
            var graph = new AdjacencyGraph<char, TaggedEquatableEdge<char, double>>(true);
            graph.AddVertexRange("1");
            var yen = new YenShortestPathsAlgorithm<char>(graph, '1', '1', 10);
            graph.AddEdge(new TaggedEquatableEdge<char, double>('1', '1', 7));
            var exeptionWas = false;
            try
            {
                yen.Execute();
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, e is InstanceNotFoundException);
                exeptionWas = true;
            }
            Assert.AreEqual(exeptionWas, true);
        }

        [TestMethod]
        public void YenNormalCaseTest()
        {
            /* generate simple graph
              like this https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
              but with directed edgesinput.Graph
            */
            var input = GenerateNormalInput();
            var yen = new YenShortestPathsAlgorithm<char>(input, '1', '5', 10);
            var result = yen.Execute().ToList();

            /*
            Expecting to get 3 paths:
            1. 1-3-4-5
            2. 1-2-4-5
            3. 1-2-3-4-5
            Consistently checking the result
            */
            Assert.AreEqual(3, result.Count);
            // 1.
            Assert.AreEqual(result[0].ToArray()[0], input.Edges.ToArray()[1]);
            Assert.AreEqual(result[0].ToArray()[1], input.Edges.ToArray()[5]);
            Assert.AreEqual(result[0].ToArray()[2], input.Edges.ToArray()[7]);
            // 2.
            Assert.AreEqual(result[1].ToArray()[0], input.Edges.ToArray()[0]);
            Assert.AreEqual(result[1].ToArray()[1], input.Edges.ToArray()[4]);
            Assert.AreEqual(result[1].ToArray()[2], input.Edges.ToArray()[7]);
            // 3.
            Assert.AreEqual(result[2].ToArray()[0], input.Edges.ToArray()[0]);
            Assert.AreEqual(result[2].ToArray()[1], input.Edges.ToArray()[3]);
            Assert.AreEqual(result[2].ToArray()[2], input.Edges.ToArray()[5]);
            Assert.AreEqual(result[2].ToArray()[3], input.Edges.ToArray()[7]);
        }

        private AdjacencyGraph<char, TaggedEquatableEdge<char, double>> GenerateNormalInput()
        {
            var graph = new AdjacencyGraph<char, TaggedEquatableEdge<char, double>>(true);
            graph.AddVertexRange("123456");
            graph.AddEdge(new TaggedEquatableEdge<char, double>('1', '2', 7)); // 0
            graph.AddEdge(new TaggedEquatableEdge<char, double>('1', '3', 9)); // 1
            graph.AddEdge(new TaggedEquatableEdge<char, double>('1', '6', 14)); // 2
            graph.AddEdge(new TaggedEquatableEdge<char, double>('2', '3', 10)); // 3
            graph.AddEdge(new TaggedEquatableEdge<char, double>('2', '4', 15)); // 4
            graph.AddEdge(new TaggedEquatableEdge<char, double>('3', '4', 11)); // 5
            graph.AddEdge(new TaggedEquatableEdge<char, double>('3', '6', 2)); // 6
            graph.AddEdge(new TaggedEquatableEdge<char, double>('4', '5', 6)); // 7
            graph.AddEdge(new TaggedEquatableEdge<char, double>('5', '6', 9)); // 8
            return graph;
        }
    }
}