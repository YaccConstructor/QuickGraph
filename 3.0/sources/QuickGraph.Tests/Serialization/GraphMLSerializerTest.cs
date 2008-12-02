using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Serialization
{
    public static class GraphMLFilesHelper
    {
        public static string[] GetFileNames()
        {
            return Directory.GetFiles("GraphML", "*.graphml");
        }
    }

    [TestFixture, PexClass]
    [CurrentFixture]
    public partial class GraphMLSerializerTest
    {
        [Test]
        [Ignore("need to handle DTD")]
        public void DeserializeFromGraphMLNorth()
        {
            foreach (var graphmlFile in GraphMLFilesHelper.GetFileNames())
            {
                Console.Write(graphmlFile);
                var g = new AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
                var settings = new XmlReaderSettings();
                settings.ProhibitDtd = false;
                using (var reader = XmlReader.Create(graphmlFile, settings))
                {
                    g.DeserializeFromGraphML(
                        reader,
                        id => new IdentifiableVertex(id),
                        (source, target, id) => new IdentifiableEdge<IdentifiableVertex>(source, target, id)
                        );
                }
                Console.WriteLine(": {0} vertices, {1} edges", g.VertexCount, g.EdgeCount);
            }
        }

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

            using (var writer = new StringWriter())
            {
                using(var xwriter = XmlWriter.Create(writer))
                    g.SerializeToGraphML(xwriter);
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
            using (var sw = new StringWriter())
            {
                using (var xwriter = XmlWriter.Create(sw))
                    gd.SerializeToGraphML(xwriter);
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
