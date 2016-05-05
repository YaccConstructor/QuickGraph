using System;
using System.Collections.Generic;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace QuickGraph.Tests
{
    [TestClass()]
    public class ClusteredGraphTest
    {
        public bool ContainsVert(ClusteredAdjacencyGraph<int, IEdge<int>> clus,int v)
        {
            if (!clus.ContainsVertex(v))
            {
                return false;
            }
            else if (clus.Parent != null)
            {
                ContainsVert(clus.Parent,v);
            }
            return true;
        }

        public bool ContainsEd(ClusteredAdjacencyGraph<int, IEdge<int>> clus, IEdge<int> e)
        {
            if (!clus.ContainsEdge(e))
            {
                return false;
            }
            else if (clus.Parent != null)
            {
                ContainsEd(clus.Parent, e);
            }
            return true;
        }

        [TestMethod()]
        public void ContainsClustVertexTest1()
        {
            var ag = new AdjacencyGraph<int, IEdge<int>>();
            var cl = new ClusteredAdjacencyGraph<int, IEdge<int>>(ag);
            var cluster1 = cl.AddCluster();
            cluster1.AddVertex(5);
            var a = ContainsVert(cl, 5);
            Assert.IsTrue(a);
        }

        [TestMethod()]
        public void ContainsClustEdgeTest1()
        {
            var ag = new AdjacencyGraph<int, IEdge<int>>();
            var cl = new ClusteredAdjacencyGraph<int, IEdge<int>>(ag);
            var cluster1 = cl.AddCluster();
            cluster1.AddVertex(5);
            cluster1.AddVertex(6);
            var edge = new TaggedEdge<int, int>(5, 7, 1);
            cluster1.AddEdge(edge);
            var a = ContainsEd(cl, edge);
            Assert.IsTrue(a);
        }

    }
}

