using System;
using System.Collections.Generic;
using QuickGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace QuickGraph.Tests
{
    [TestClass()]
    public class ClusteredGraphTest
    {
        public bool ContainsVertexParent(ClusteredAdjacencyGraph<int, IEdge<int>> clus,int v)
        {
            return (clus.ContainsVertex(v) && clus.Parent!=null && ContainsVertexParent(clus.Parent,v) 
                   || clus.Parent == null);
        }

        public bool ContainsEdgeParent(ClusteredAdjacencyGraph<int, IEdge<int>> clus, IEdge<int> e)
        {
            return (clus.ContainsEdge(e) && clus.Parent != null && ContainsEdgeParent(clus.Parent, e) 
                   || clus.Parent == null);
        }

        [TestMethod()]
        public void AddingClustVertexTest1()
        {
            var graph = new AdjacencyGraph<int, IEdge<int>>();
            var clusteredGraph = new ClusteredAdjacencyGraph<int, IEdge<int>>(graph);
            var cluster1 = clusteredGraph.AddCluster();
            cluster1.AddVertex(5);
            var a = ContainsVertexParent(clusteredGraph, 5);
            Assert.IsTrue(a);
        }

        [TestMethod()]
        public void AddingClustEdgeTest1()
        {
            var graph = new AdjacencyGraph<int, IEdge<int>>();
            var clusteredGraph = new ClusteredAdjacencyGraph<int, IEdge<int>>(graph);
            var cluster1 = clusteredGraph.AddCluster();
            cluster1.AddVertex(5);
            cluster1.AddVertex(6);
            var edge = new TaggedEdge<int, int>(5, 6, 1);
            cluster1.AddEdge(edge);
            var a = ContainsEdgeParent(clusteredGraph, edge);
            Assert.IsTrue(a);
        }

          [TestMethod()]
          public void RemovingClustEdgeTest1()
          {
              var graph = new AdjacencyGraph<int, IEdge<int>>();
              var clusteredGraph = new ClusteredAdjacencyGraph<int, IEdge<int>>(graph);
              var cluster1 = clusteredGraph.AddCluster();
              var cluster2 = cluster1.AddCluster();
              var cluster3 = cluster2.AddCluster();
            cluster3.AddVertex(5);
            cluster3.AddVertex(6);
            var edge = new TaggedEdge<int, int>(5, 6, 1);
            cluster1.RemoveEdge(edge);
            Assert.IsFalse(ContainsEdgeParent(cluster2, edge));
          }
          
        [TestMethod()]
        public void RemovingClustVertexTest1()
        {
            var graph = new AdjacencyGraph<int, IEdge<int>>();
            var clusteredGraph = new ClusteredAdjacencyGraph<int, IEdge<int>>(graph);
            var cluster1 = clusteredGraph.AddCluster();
            var cluster2 = cluster1.AddCluster();
            var cluster3 = cluster2.AddCluster();
            cluster3.AddVertex(5);
            cluster2.RemoveVertex(5);
            Assert.IsFalse(ContainsVertexParent(cluster3, 5));
        }
    }
}

