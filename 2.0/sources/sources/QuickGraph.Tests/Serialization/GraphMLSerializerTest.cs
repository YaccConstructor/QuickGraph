using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Serialization
{
    [TestFixture, PexClass]
    public partial class GraphMLSerializerTest
    {
        [Test, PexMethod]
        public void RoundTrip()
        {
            RoundTripGraph(new AdjacencyGraphFactory().SimpleIdentifiable());
        }

        [PexMethod]
        public void RoundTripGraph([PexAssumeNotNull]IMutableVertexAndEdgeListGraph<NamedVertex, NamedEdge> g)
        {
            GraphMLSerializer<NamedVertex, NamedEdge> serializer = new GraphMLSerializer<NamedVertex, NamedEdge>();
            AdjacencyGraph<NamedVertex, NamedEdge> gd = new AdjacencyGraph<NamedVertex, NamedEdge>();
            string baseLine;
            string output;

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, g);
                baseLine = writer.ToString();
                TestConsole.WriteLineBold("Original graph:");
                Console.WriteLine(writer.ToString());
                TestConsole.WriteLineBold("---");

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

            TestConsole.WriteLineBold("Roundtripped graph:");
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, gd);
                output = sw.ToString();
                Console.WriteLine(sw.ToString());
            }

            Assert.AreEqual(g.VertexCount, gd.VertexCount);
            Assert.AreEqual(g.EdgeCount, gd.EdgeCount);
            StringAssert.AreEqual(
                baseLine,
                output,
                StringComparison.InvariantCulture
                );
        }
    }
}
