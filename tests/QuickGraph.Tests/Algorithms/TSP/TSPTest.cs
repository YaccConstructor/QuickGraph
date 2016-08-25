using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.TSP;
using System.IO;
using QuickGraph;
using QuickGraph.Algorithms;
using System.Diagnostics;

namespace QuickGraph.Tests.Algorithms.TSP
{
    [TestClass]
    public class TSPTest
    {

        [TestMethod]
        public void UndirectedFullGraph()
        {
            TestCase testCase = new TestCase();
            testCase.AddVertex("n1")
                .AddVertex("n2")
                .AddVertex("n3")
                .AddVertex("n4")
                .AddVertex("n5");

            testCase.AddUndirectedEdge("n1", "n2", 16)
                .AddUndirectedEdge("n1", "n3", 9)
                .AddUndirectedEdge("n1", "n4", 15)
                .AddUndirectedEdge("n1", "n5", 3)
                .AddUndirectedEdge("n2", "n3", 14)
                .AddUndirectedEdge("n2", "n4", 4)
                .AddUndirectedEdge("n2", "n5", 5)
                .AddUndirectedEdge("n3", "n4", 4)
                .AddUndirectedEdge("n3", "n5", 2)
                .AddUndirectedEdge("n4", "n5", 1);

            var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(testCase.Graph, testCase.GetFuncWeights());

            tcp.Compute();
            Assert.AreEqual(tcp.BestCost, 25);
            Assert.IsFalse(tcp.ResultPath.IsDirectedAcyclicGraph());
        }
        [TestMethod]
        public void UndirectedSparseGraph()
        {
            TestCase testCase = new TestCase();
            testCase.AddVertex("n1")
                .AddVertex("n2")
                .AddVertex("n3")
                .AddVertex("n4")
                .AddVertex("n5")
                .AddVertex("n6");

            testCase.AddUndirectedEdge("n1", "n2", 10)
                .AddUndirectedEdge("n2", "n3", 8)
                .AddUndirectedEdge("n3", "n4", 11)
                .AddUndirectedEdge("n4", "n5", 6)
                .AddUndirectedEdge("n5", "n6", 9)
                .AddUndirectedEdge("n1", "n6", 3)
                .AddUndirectedEdge("n2", "n6", 5)
                .AddUndirectedEdge("n3", "n6", 18)
                .AddUndirectedEdge("n3", "n5", 21);

            var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(testCase.Graph, testCase.GetFuncWeights());
            tcp.Compute();

            Assert.AreEqual(tcp.BestCost, 47);
            Assert.IsFalse(tcp.ResultPath.IsDirectedAcyclicGraph());
        }
        [TestMethod]
        public void DirectedSparseGraphWithoutPath()
        {
            TestCase testCase = new TestCase();
            testCase.AddVertex("n1")
                .AddVertex("n2")
                .AddVertex("n3")
                .AddVertex("n4")
                .AddVertex("n5")
                .AddVertex("n6");

            testCase.AddDirectedEdge("n1", "n2", 10)
                .AddDirectedEdge("n2", "n3", 8)
                .AddDirectedEdge("n3", "n4", 11)
                .AddDirectedEdge("n4", "n5", 6)
                .AddDirectedEdge("n5", "n6", 9)
                .AddDirectedEdge("n1", "n6", 3)
                .AddDirectedEdge("n2", "n6", 5)
                .AddDirectedEdge("n3", "n6", 18)
                .AddDirectedEdge("n3", "n5", 21);

            var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(testCase.Graph, testCase.GetFuncWeights());
            tcp.Compute();

            Assert.AreEqual(tcp.BestCost, Double.PositiveInfinity);
            Assert.IsTrue(tcp.ResultPath == null);
        }
        [TestMethod]
        public void DirectedSparseGraph()
        {
            TestCase testCase = new TestCase();
            testCase.AddVertex("n1")
                .AddVertex("n2")
                .AddVertex("n3")
                .AddVertex("n4")
                .AddVertex("n5")
                .AddVertex("n6");

            testCase.AddDirectedEdge("n1", "n2", 10)
                .AddDirectedEdge("n2", "n3", 8)
                .AddDirectedEdge("n3", "n4", 11)
                .AddDirectedEdge("n4", "n5", 6)
                .AddDirectedEdge("n5", "n6", 9)
                .AddDirectedEdge("n1", "n6", 3)
                .AddDirectedEdge("n2", "n6", 5)
                .AddDirectedEdge("n3", "n6", 18)
                .AddDirectedEdge("n3", "n5", 21)
                .AddDirectedEdge("n6", "n1", 1);

            var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(testCase.Graph, testCase.GetFuncWeights());
            tcp.Compute();

            Assert.AreEqual(tcp.BestCost, 45);
            Assert.IsFalse(tcp.ResultPath.IsDirectedAcyclicGraph());
        }

        //[TestMethod]
        public void performanceTest()
        {
            for (int i = 0; i < 6; ++i)
            {
                int repeat = 10;
                long avg = 0;
                for (int j = 0; j < repeat; ++j)
                {
                    TestCase testCase = TestCase.completeGraphTestCase((i + 1) * 5, 100000000);
                    var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(testCase.Graph, testCase.GetFuncWeights());
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    tcp.Compute();
                    stopWatch.Stop();
                    avg += stopWatch.ElapsedMilliseconds;
                }
                Trace.WriteLine((i + 1) * 5 + " vertices complete, avg time: " + avg / repeat);
            }
        }
    }

    public class TestCase
    {
        public BidirectionalGraph<String, EquatableEdge<String>> Graph = new BidirectionalGraph<String, EquatableEdge<String>>();
        public Dictionary<EquatableEdge<String>, double> WeightsDict = new Dictionary<EquatableEdge<String>, double>();

        public TestCase AddVertex(String vertex)
        {
            Graph.AddVertex(vertex);

            return this;
        }

        public TestCase AddUndirectedEdge(String source, String target, double weight)
        {
            AddDirectedEdge(source, target, weight);
            AddDirectedEdge(target, source, weight);

            return this;
        }

        public TestCase AddDirectedEdge(String source, String target, double weight)
        {
            var e = new EquatableEdge<String>(source, target);
            Graph.AddEdge(e);
            WeightsDict.Add(e, weight);

            return this;
        }

        public Func<EquatableEdge<String>, double> GetFuncWeights()
        {
            return (edge => WeightsDict[edge]);
        }

        public static TestCase completeGraphTestCase(int vertices, int maxWeight)
        {
            var random = new Random();
            TestCase testCase = new TestCase();
            for (int i = 0; i < vertices; ++i)
            {
                testCase.AddVertex("n" + i);
            }
            foreach (var v1 in testCase.Graph.Vertices)
            {
                foreach (var v2 in testCase.Graph.Vertices)
                {
                    testCase.AddDirectedEdge(v1, v2, random.Next(maxWeight));
                }
            }
            return testCase;
        }
    }

}
 