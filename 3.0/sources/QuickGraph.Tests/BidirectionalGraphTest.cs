using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QuickGraph.Tests
{
    
    
    /// <summary>
    ///This is a test class for BidirectionalGraphTest and is intended
    ///to contain all BidirectionalGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BidirectionalGraphTest
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


        /// <summary>
        ///A test for Clone
        ///</summary>
        public void CloneTestHelper<TVertex, TEdge>()

            where TEdge : IEdge<TVertex>
        {
            BidirectionalGraph<TVertex, TEdge> target = new BidirectionalGraph<TVertex, TEdge>(); // TODO: Initialize to an appropriate value
            BidirectionalGraph<TVertex, TEdge> expected = null; // TODO: Initialize to an appropriate value
            BidirectionalGraph<TVertex, TEdge> actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void CloneTest()
        {
            var g = new BidirectionalGraph<int, Edge<int>>();
            g.AddVertexRange(new int[3] {1, 2, 3});
            g.AddEdge(new Edge<int>(1, 2));
            g.AddEdge(new Edge<int>(2, 3));
            g.AddEdge(new Edge<int>(3, 1));

            Assert.AreEqual(3, g.VertexCount);
            Assert.AreEqual(3, g.EdgeCount);

            var h = g.Clone();

            Assert.AreEqual(3, h.VertexCount);
            Assert.AreEqual(3, h.EdgeCount);

            h.AddVertexRange(new int[4] { 10, 11, 12, 13 });
            h.AddEdge(new Edge<int>(10, 11));

            Assert.AreEqual(7, h.VertexCount);
            Assert.AreEqual(4, h.EdgeCount);

            var i = 0;
            foreach (var e in h.Edges)
                i++;

            Assert.AreEqual(4, i);

            Assert.AreEqual(3, g.VertexCount);
            Assert.AreEqual(3, g.EdgeCount);
        }
    }
}
