using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using QuickGraph.Serialization.DirectedGraphML;
using System.Xml;
using QuickGraph.Algorithms;
using System.Diagnostics;

namespace QuickGraph.Tests.Serialization
{
    [TestClass]
    public partial class DirectedGraphMLExtensionsTest
    {
        [TestMethod]
        public void SimpleGraph()
        {
            int[][] edges = { new int[]{ 1, 2, 3 }, 
                              new int[]{ 2, 3, 1 } };
            edges.ToAdjacencyGraph()
                .ToDirectedGraphML()
                .WriteXml("simple.dgml");

            if (Debugger.IsAttached)
            { 
                Process.Start("simple.dgml");
            }

            edges.ToAdjacencyGraph()
                .ToDirectedGraphML()
                .WriteXml(Console.Out);
        }

        [TestMethod]
        public void ToDirectedGraphML()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
            {
                var dg = g.ToDirectedGraphML();
                Assert.IsNotNull(g);
                Assert.AreEqual(dg.Nodes.Length, g.VertexCount);
                Assert.AreEqual(dg.Links.Length, g.EdgeCount);
            }
        }
    }
}
