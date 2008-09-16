using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Tests
{
    [TestFixture, PexClass]
    public partial class DegreeTest
    {
        [PexMethod]
        public void DegreeSumEqualsTwiceEdgeCount(
            [PexAssumeNotNull]IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.Degree(v);

            Assert.AreEqual(edgeCount * 2, degCount);
        }

        [PexMethod]
        public void InDegreeSumEqualsEdgeCount([PexAssumeNotNull] IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.InDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

        [PexMethod]
        public void OutDegreeSumEqualsEdgeCount([PexAssumeNotNull] IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.OutDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

    }
}
