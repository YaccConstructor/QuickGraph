using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using Microsoft.Pex.Framework.Settings;

namespace QuickGraph.Tests
{
    [TestClass, PexClass]
    public partial class DegreeTest
    {
        [TestMethod]
        public void DegreeSumEqualsTwiceEdgeCountAll()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
                this.DegreeSumEqualsTwiceEdgeCount(g);
        }

        [PexMethod]
        public void DegreeSumEqualsTwiceEdgeCount<TVertex, TEdge>(
            [PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> graph)
            where TEdge : IEdge<TVertex>
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (var v in graph.Vertices)
                degCount += graph.Degree(v);

            Assert.AreEqual(edgeCount * 2, degCount);
        }

        [TestMethod]
        public void InDegreeSumEqualsEdgeCountAll()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
                this.InDegreeSumEqualsEdgeCount(g);
        }

        [PexMethod]
        public void InDegreeSumEqualsEdgeCount<TVertex,TEdge>(
            [PexAssumeNotNull] IBidirectionalGraph<TVertex, TEdge> graph)
            where TEdge : IEdge<TVertex>
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (var v in graph.Vertices)
                degCount += graph.InDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

        [TestMethod]
        public void OutDegreeSumEqualsEdgeCountAll()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
                this.OutDegreeSumEqualsEdgeCount(g);
        }

        [PexMethod]
        public void OutDegreeSumEqualsEdgeCount<TVertex,TEdge>(
            [PexAssumeNotNull] IBidirectionalGraph<TVertex, TEdge> graph)
            where TEdge : IEdge<TVertex>
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (var v in graph.Vertices)
                degCount += graph.OutDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

    }
}
