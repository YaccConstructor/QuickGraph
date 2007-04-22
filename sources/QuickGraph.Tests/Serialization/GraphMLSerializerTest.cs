using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MbUnit.Framework;

namespace QuickGraph.Serialization
{
    [TestFixture]
    public sealed class GraphMLSerializerTest
    {
        [Test]
        public void RoundTrip()
        {
            RoundTripGraph(new AdjacencyGraphFactory().SimpleIdentifiable());
        }

        private void RoundTripGraph(IMutableVertexAndEdgeListGraph<NamedVertex, NamedEdge> g)
        {
            GraphMLSerializer<NamedVertex, NamedEdge> serializer = new GraphMLSerializer<NamedVertex, NamedEdge>();
            AdjacencyGraph<NamedVertex, NamedEdge> gd = new AdjacencyGraph<NamedVertex, NamedEdge>();
            string baseLine;
            string output;

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, g);
                baseLine = writer.ToString();
                Console.WriteLine(writer.ToString());

                using (XmlTextReader reader = new XmlTextReader(new StringReader(writer.ToString())))
                {
                    serializer.Deserialize(
                        reader,
                        gd,
                        new NamedVertex.Factory(),
                        new NamedEdge.Factory()
                    );
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, gd);
                output = sw.ToString();
                Console.WriteLine(sw.ToString());
            }

            Assert.AreEqual(g.VertexCount, gd.VertexCount);
            Assert.AreEqual(g.EdgeCount, gd.EdgeCount);
            StringAssert.AreEqualIgnoreCase(
                baseLine,
                output
                );
        }
    }
}
