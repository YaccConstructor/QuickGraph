using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Serialization
{
    [TestFixture, PexClass]
    [CurrentFixture]
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
            AdjacencyGraph<NamedVertex, NamedEdge> gd = new AdjacencyGraph<NamedVertex, NamedEdge>();
            string baseLine;
            string output;

            using (StringWriter writer = new StringWriter())
            {
                g.SerializeToGraphML(writer);
                baseLine = writer.ToString();
                TestConsole.WriteLineBold("Original graph:");
                Console.WriteLine(writer.ToString());
                TestConsole.WriteLineBold("---");

                using (XmlTextReader reader = new XmlTextReader(new StringReader(writer.ToString())))
                {
                    gd.DeserializeFromGraphML(reader,
                        id => new NamedVertex(id),
                        (source, target, id) => new NamedEdge(source, target, id)
                    );
                }
            }

            TestConsole.WriteLineBold("Roundtripped graph:");
            using (StringWriter sw = new StringWriter())
            {
                gd.SerializeToGraphML(sw);
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
