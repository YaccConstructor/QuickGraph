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
        [PexTest]
        public void DegreeSumEqualsTwiceEdgeCount([PexAssumeIsNotNull] IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.Degree(v);

            Assert.AreEqual(edgeCount * 2, degCount);
        }

        [PexTest]
        public void InDegreeSumEqualsEdgeCount([PexAssumeIsNotNull] IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.InDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

        [PexTest]
        public void OutDegreeSumEqualsEdgeCount([PexAssumeIsNotNull] IBidirectionalGraph<string, Edge<string>> graph)
        {
            int edgeCount = graph.EdgeCount;
            int degCount = 0;
            foreach (string v in graph.Vertices)
                degCount += graph.OutDegree(v);

            Assert.AreEqual(edgeCount, degCount);
        }

    }
}
