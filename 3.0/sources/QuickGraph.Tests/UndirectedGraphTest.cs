using System;
using System.Linq;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests
{
    /// <summary>
    ///This is a test class for UndirectedGraph and is intended
    ///to contain all UndirectedGraph Unit Tests
    ///</summary>
    [TestClass()]
    public class UndirectedGraphTest
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
            var u = new UndirectedGraph<int, SEquatableUndirectedEdge<int>>();
            var e1 = new SEquatableUndirectedEdge<int>(1, 2);
            var e2 = new SEquatableUndirectedEdge<int>(1, 2);
            
            bool exceptionOccurred = false;
            try
            {
                new SEquatableUndirectedEdge<int>(2, 1);
            }
            catch (ArgumentException e)
            {
                exceptionOccurred = true;
            }
            Assert.IsTrue(exceptionOccurred);

            u.AddVerticesAndEdge(e1);

            Assert.AreEqual(1, u.AdjacentDegree(1));
            Assert.AreEqual(1, u.AdjacentDegree(2));
            Assert.AreEqual(1, u.AdjacentEdges(1).Count());
            Assert.AreEqual(1, u.AdjacentEdges(2).Count());

            Assert.IsTrue(u.ContainsEdge(e1));
            Assert.IsTrue(u.ContainsEdge(e2));
            Assert.IsTrue(u.ContainsEdge(1, 2));
            Assert.IsTrue(u.ContainsEdge(2, 1));
            Assert.IsFalse(u.ContainsEdge(1, 3));   
        }

        [TestMethod()]
        public void ContainsEdgeTest2()
        {
            var u = new UndirectedGraph<int, EquatableEdge<int>>();
            var e1 = new EquatableEdge<int>(1, 2);
            var e2 = new EquatableEdge<int>(2, 1);

            u.AddVerticesAndEdge(e1);

            Assert.AreEqual(1, u.AdjacentDegree(1));
            Assert.AreEqual(1, u.AdjacentDegree(2));
            Assert.AreEqual(1, u.AdjacentEdges(1).Count());
            Assert.AreEqual(1, u.AdjacentEdges(2).Count());

            Assert.IsTrue(u.ContainsEdge(e1));
            Assert.IsFalse(u.ContainsEdge(e2));
            Assert.IsTrue(u.ContainsEdge(1, 2));
            Assert.IsTrue(u.ContainsEdge(2, 1));
            Assert.IsFalse(u.ContainsEdge(1, 3));
        }
    }
}
