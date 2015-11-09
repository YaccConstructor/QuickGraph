using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace QuickGraph.DotParserTest
{
    [TestClass]
    public class DotParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "strict graph t { 6 [lable = \"v1\"] \n 1 -- 2 -- 3 \n 2 -- 1 \n 1 -- 1 \n 3 -- 4 \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);
            
            Assert.AreEqual(5, g1.EdgeCount);
            Assert.AreEqual(5, g1.VertexCount);
            Assert.IsTrue(g1.ContainsEdge("1","2"));
            Assert.IsTrue(g1.ContainsEdge("2", "3"));
            Assert.IsTrue(g1.ContainsEdge("1", "1"));
            Assert.IsTrue(g1.ContainsEdge("3", "4"));
            Assert.IsTrue(g1.ContainsVertex("6"));

        }
        [TestMethod]
        public void TestMethod2()
        {
            var adr = "..\\..\\..\\test_inputs\\test1.dot";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g2 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotFile(adr, f, f2);
            
            Assert.AreEqual(4, g2.EdgeCount);
            Assert.AreEqual(5, g2.VertexCount);
    
        
        
        }
        [TestMethod]
        public void TestMethod3()
        {
            var str = "strict graph t { 1 \n 1 -- 1 \n 1 -- 1 \n 1 -- 1; \n 1 -- 1 \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(4, g1.EdgeCount);
            Assert.AreEqual(1, g1.VertexCount);


        }
        [TestMethod]
        public void TestMethod4()
        {
            var str = "strict graph t { 1 \n 2 \n 3 \n 4 \n 5 \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(0, g1.EdgeCount);
            Assert.AreEqual(5, g1.VertexCount);
            Assert.IsFalse(g1.ContainsEdge("1", "2"));
            Assert.IsFalse(g1.ContainsEdge("2", "3"));
            Assert.IsFalse(g1.ContainsEdge("3", "4"));
            Assert.IsFalse(g1.ContainsEdge("4", "5"));

        }
        [TestMethod]
        public void TestMethod5()
        {
            var str = "strict graph t { 9 \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(0, g1.EdgeCount);
            Assert.AreEqual(1, g1.VertexCount);
            Assert.IsTrue(g1.ContainsVertex("9"));
            Assert.IsFalse(g1.ContainsEdge("9","9"));


        }
        [TestMethod]
        public void TestMethod6()
        {
            var str = "strict graph t { 8 \n  3 -> 6 \n 6 -> 1 \n 9 -> 10; \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SEdge<string>> f2 = (v1, v2, attrs) => new SEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(3, g1.EdgeCount);
            Assert.AreEqual(6, g1.VertexCount);
            Assert.IsTrue(g1.ContainsVertex("8"));
            Assert.IsTrue(g1.ContainsEdge("3", "6"));
            Assert.IsTrue(g1.ContainsEdge("6", "1"));
            Assert.IsTrue(g1.ContainsEdge("9", "10"));

        }

        [TestMethod]
        public void TestMethod7()
        {
            var str = "strict graph t { 1 -- 2 -- 3 -- 4 -- 6 -- 7 \n 2 -- 1 \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(6, g1.EdgeCount);
            Assert.AreEqual(6, g1.VertexCount);
            Assert.IsTrue(g1.ContainsEdge("1", "2"));
            Assert.IsTrue(g1.ContainsEdge("2", "3"));
            Assert.IsTrue(g1.ContainsEdge("3", "4"));
            Assert.IsTrue(g1.ContainsEdge("4", "6"));
            Assert.IsTrue(g1.ContainsEdge("6", "7"));


        }
        [TestMethod]
        public void TestMethod8()
        {
            var str = "strict graph t { 1 -- 2 [weight=9] \n 1 -- 2 [weight=10] \n 2 -- 1[weight=11]; \n 2 -- 1[weight=12] \n 1 [label=\"v1\"] \n 2 [label=\"v2\"] \n }";
            Func<string, Tuple<string, string>[], string> f = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g1 = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f, f2);

            Assert.AreEqual(4, g1.EdgeCount);
            Assert.AreEqual(2, g1.VertexCount);


        }
    }
}
