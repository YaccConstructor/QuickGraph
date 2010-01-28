using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using QuickGraph.Serialization.DirectedGraphML;

namespace QuickGraph.Tests.Serialization
{
    [TestClass]
    public partial class DirectedGraphMLExtensionsTest
    {
        [TestMethod]
        public void ToDirectedGraphML()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
            {
                var dg = g.ToDirectedGraphML();
                Assert.IsNotNull(g);
            }
        }
    }
}
