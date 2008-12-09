using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Pex.Framework;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Serialization
{
    public static class TestGraphFactory
    {
        public static string[] GetFileNames()
        {
            return Directory.GetFiles(".", "*.graphml");
        }

        public static IEnumerable<BidirectionalGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>> GetBidirectionalGraphs()
        {
            yield return new BidirectionalGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
            foreach (var graphmlFile in TestGraphFactory.GetFileNames())
            {
                var g = LoadBidirectionalGraph(graphmlFile);
                yield return g;
            }
        }

        public static BidirectionalGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> LoadBidirectionalGraph(string graphmlFile)
        {
            Console.WriteLine(graphmlFile);
            var g = new BidirectionalGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
            using (var reader = new StreamReader(graphmlFile))
            {
                g.DeserializeFromGraphML(
                    reader,
                    id => new IdentifiableVertex(id),
                    (source, target, id) => new IdentifiableEdge<IdentifiableVertex>(source, target, id)
                    );
            }
            return g;
        }

        public static IEnumerable<AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>> GetAdjacencyGraphs()
        {
            yield return new AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
            foreach (var graphmlFile in TestGraphFactory.GetFileNames())
            {
                var g = LoadGraph(graphmlFile);
                yield return g;
            }
        }

        public static AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> LoadGraph(string graphmlFile)
        {
            Console.WriteLine(graphmlFile);
            var g = new AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
            using (var reader = new StreamReader(graphmlFile))
            {
                g.DeserializeFromGraphML(
                    reader,
                    id => new IdentifiableVertex(id),
                    (source, target, id) => new IdentifiableEdge<IdentifiableVertex>(source, target, id)
                    );
            }
            return g;
        }

        public static IEnumerable<UndirectedGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>> GetUndirectedGraphs()
        {
            yield return new UndirectedGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
            foreach (var g in GetAdjacencyGraphs())
            {
                var ug = new UndirectedGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
                foreach (var v in g.Vertices)
                    ug.AddVertex(v);
                foreach (var e in g.Edges)
                    if (!ug.ContainsEdge(e.Source, e.Target))
                        ug.AddEdge(e);
                yield return ug;
            }
        }
    }

    [TestClass, PexClass]
    public partial class GraphMLSerializerIntegrationTest
    {
        [TestMethod]
        public void DeserializeFromGraphMLNorth()
        {
            foreach (var graphmlFile in TestGraphFactory.GetFileNames())
            {
                Console.Write(graphmlFile);
                var g = new AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>();
                using (var reader = new StreamReader(graphmlFile))
                {
                    g.DeserializeFromGraphML(
                        reader,
                        id => new IdentifiableVertex(id),
                        (source, target, id) => new IdentifiableEdge<IdentifiableVertex>(source, target, id)
                        );
                }
                Console.Write(": {0} vertices, {1} edges", g.VertexCount, g.EdgeCount);

                var vertices = new Dictionary<string, IdentifiableVertex>();
                foreach(var v in g.Vertices)
                    vertices.Add(v.ID, v);

                // check all nodes are loaded
                var settings = new XmlReaderSettings();
                settings.XmlResolver = new GraphMLXmlResolver();
                settings.ProhibitDtd = false;
                settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None;
                using(var xreader = XmlReader.Create(graphmlFile, settings))
                {
                    var doc = new XPathDocument(xreader);
                    foreach (XPathNavigator node in doc.CreateNavigator().Select("/graphml/graph/node"))
                    {
                        string id = node.GetAttribute("id", "");
                        Assert.IsTrue(vertices.ContainsKey(id));
                    }
                    Console.Write(", vertices ok");

                    // check all edges are loaded
                    foreach (XPathNavigator node in doc.CreateNavigator().Select("/graphml/graph/edge"))
                    {
                        string source = node.GetAttribute("source", "");
                        string target = node.GetAttribute("target", "");
                        Assert.IsTrue(g.ContainsEdge(vertices[source], vertices[target]));
                    }
                    Console.Write(", edges ok");
                }
                Console.WriteLine();
            }
        }
    }
}
