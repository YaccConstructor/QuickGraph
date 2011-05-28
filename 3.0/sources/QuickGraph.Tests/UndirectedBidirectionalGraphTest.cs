using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QuickGraph.Tests
{
    
    
    /// <summary>
    ///This is a test class for UndirectedBidirectionalGraphTest and is intended
    ///to contain all UndirectedBidirectionalGraphTest Unit Tests
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
        public void ContainsEdgeTest()
        {
            var u = new UndirectedGraph<int, SEquatableUndirectedEdge<int>>();
            u.AddVerticesAndEdge(new SEquatableUndirectedEdge<int>(1, 2));
            Assert.IsTrue(u.ContainsEdge(1, 2));

            var failure = false;
            try
            {
                u.ContainsEdge(2, 1);
            } catch
            {
                failure = true;
            }
            Assert.IsTrue(failure);

            Assert.IsTrue(u.ContainsEdge(new SEquatableUndirectedEdge<int>(1, 2)));

            failure = false;
            try
            {
                var e = new SEquatableUndirectedEdge<int>(2, 1);
            }
            catch
            {
                failure = true;
            }
            Assert.IsTrue(failure);

            var bd = new BidirectionalGraph<int, EquatableEdge<int>>();
            bd.AddVerticesAndEdge(new EquatableEdge<int>(1, 2));

            Assert.IsTrue(bd.ContainsEdge(1, 2));
            Assert.IsFalse(bd.ContainsEdge(2, 1));
            Assert.IsTrue(bd.ContainsEdge(new EquatableEdge<int>(1, 2)));
            Assert.IsFalse(bd.ContainsEdge(new EquatableEdge<int>(2, 1)));

            var ubd = new UndirectedBidirectionalGraph<int, EquatableEdge<int>>(bd);

            Assert.IsTrue(ubd.ContainsEdge(1, 2));
            Assert.IsTrue(ubd.ContainsEdge(2, 1));
            Assert.IsTrue(ubd.ContainsEdge(new EquatableEdge<int>(1, 2)));
            Assert.IsFalse(ubd.ContainsEdge(new EquatableEdge<int>(2, 1)));
        }
    }
}
