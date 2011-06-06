using System;
using System.Linq;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests
{
    /// <summary>
    ///This is a test class for UndirectedBidirectionalGraph and is intended
    ///to contain all UndirectedBidirectionalGraph Unit Tests
    ///</summary>
    [TestClass()]
    public class UndirectedBidirectionalGraphTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void ContainsEdgeTest1()
        {
            var bd = new BidirectionalGraph<int, IEdge<int>>();

            var e12 = new SEquatableUndirectedEdge<int>(1, 2);
            var f12 = new SEquatableUndirectedEdge<int>(1, 2);

            bd.AddVerticesAndEdge(e12);

            ContainsEdgeAssertions(bd, e12, f12, null, null);

            var u = new UndirectedBidirectionalGraph<int, IEdge<int>>(bd);

            UndirectedGraphTest.ContainsEdgeAssertions(u, e12, f12, null, null);
        }

        [TestMethod()]
        public void ContainsEdgeTest2()
        {
            var bd = new BidirectionalGraph<int, IEdge<int>>();

            var e12 = new EquatableEdge<int>(1, 2);
            var f12 = new EquatableEdge<int>(1, 2);
            var e21 = new EquatableEdge<int>(2, 1);
            var f21 = new EquatableEdge<int>(2, 1);

            bd.AddVerticesAndEdge(e12);

            ContainsEdgeAssertions(bd, e12, f12, e21, f21);

            var u = new UndirectedBidirectionalGraph<int, IEdge<int>>(bd);

            UndirectedGraphTest.ContainsEdgeAssertions(u, e12, f12, e21, f21);
        }

        public static void ContainsEdgeAssertions(IBidirectionalGraph<int, IEdge<int>> g,
            IEdge<int> e12,
            IEdge<int> f12,
            IEdge<int> e21,
            IEdge<int> f21)
        {
            Assert.AreEqual(0, g.InDegree(1));
            Assert.AreEqual(1, g.OutDegree(1));
            Assert.AreEqual(1, g.InDegree(2));
            Assert.AreEqual(0, g.OutDegree(2));
            Assert.AreEqual(1, g.OutEdges(1).Count());
            Assert.AreEqual(1, g.InEdges(2).Count());

            // e12 must be present in u, because we added it.
            Assert.IsTrue(g.ContainsEdge(e12));

            // f12 is also in u, because e12 == f12.
            Assert.IsTrue(g.ContainsEdge(f12));

            // e21 and f21 are not in u, because it's a directed graph.
            if (e21 != null) Assert.IsFalse(g.ContainsEdge(e21));
            if (f21 != null) Assert.IsFalse(g.ContainsEdge(f21));

            // there must be an edge between vertices 1, 2.
            Assert.IsTrue(g.ContainsEdge(1, 2));

            // no edge between vertices 2, 1, because the graph is directed.
            Assert.IsFalse(g.ContainsEdge(2, 1));

            // ContainsEdge(1, 3) raises contracts violation in IIncidenceGraphContract, because 3 is not in the graph.
            // obviously no edge between vertices 1, 3, as vertex 3 is not even present in the graph.
            // Assert.IsFalse(g.ContainsEdge(1, 3));
        }
    }
}
