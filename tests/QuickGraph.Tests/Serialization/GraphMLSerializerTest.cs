using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Pex.Framework;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Collections;

namespace QuickGraph.Serialization
{

    public static class TestGraphFactory
    {
        public static IEnumerable<string> GetFileNames()
        {
            var list = new List<string>();
            list.AddRange(Directory.GetFiles(".", "g.*.graphml"));
            if (Directory.Exists("graphml"))
                list.AddRange(Directory.GetFiles("graphml", "g.*.graphml"));
            return list;
        }

        public static IEnumerable<BidirectionalGraph<string, Edge<string>>> GetBidirectionalGraphs()
        {
            yield return new BidirectionalGraph<string, Edge<string>>();
            foreach (var graphmlFile in TestGraphFactory.GetFileNames())
            {
                var g = LoadBidirectionalGraph(graphmlFile);
                yield return g;
            }
        }

        public static BidirectionalGraph<string, Edge<string>> LoadBidirectionalGraph(string graphmlFile)
        {
            TestConsole.WriteLine(graphmlFile);
            var g = new BidirectionalGraph<string, Edge<string>>();
            using (var reader = new StreamReader(graphmlFile))
            {
                g.DeserializeFromGraphML(
                    reader,
                    id => id,
                    (source, target, id) => new Edge<string>(source, target)
                    );
            }
            return g;
        }

        public static IEnumerable<AdjacencyGraph<string, Edge<string>>> GetAdjacencyGraphs()
        {
            yield return new AdjacencyGraph<string, Edge<string>>();
            foreach (var graphmlFile in TestGraphFactory.GetFileNames())
            {
                var g = LoadGraph(graphmlFile);
                yield return g;
            }
        }

        public static AdjacencyGraph<string, Edge<string>> LoadGraph(string graphmlFile)
        {
            TestConsole.WriteLine(graphmlFile);
            var g = new AdjacencyGraph<string, Edge<string>>();
            using (var reader = new StreamReader(graphmlFile))
            {
                g.DeserializeFromGraphML(
                    reader,
                    id => id,
                    (source, target, id) => new Edge<string>(source, target)
                    );
            }
            return g;
        }

        public static IEnumerable<UndirectedGraph<string, Edge<string>>> GetUndirectedGraphs()
        {
            yield return new UndirectedGraph<string, Edge<string>>();
            foreach (var g in GetAdjacencyGraphs())
            {
                var ug = new UndirectedGraph<string, Edge<string>>();
                ug.AddVerticesAndEdgeRange(g.Edges);
                yield return ug;
            }
        }

        public static UndirectedGraph<string, Edge<string>> LoadUndirectedGraph(string graphmlFile)
        {
            var g = LoadGraph(graphmlFile);
            var ug = new UndirectedGraph<string, Edge<string>>();
            ug.AddVerticesAndEdgeRange(g.Edges);
            return ug;
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
                var g = new AdjacencyGraph<string, Edge<string>>();
                using (var reader = new StreamReader(graphmlFile))
                {
                    g.DeserializeFromGraphML(
                        reader,
                        id => id,
                        (source, target, id) => new Edge<string>(source, target)
                        );
                }
                Console.Write(": {0} vertices, {1} edges", g.VertexCount, g.EdgeCount);

                var vertices = new Dictionary<string, string>();
                foreach(var v in g.Vertices)
                    vertices.Add(v, v);

                // check all nodes are loaded
                var settings = new XmlReaderSettings();
                settings.XmlResolver = new GraphMLXmlResolver();
                settings.DtdProcessing = DtdProcessing.Ignore;
                settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None;
                using(var xreader = XmlReader.Create(graphmlFile, settings))
                {
                    var doc = new XPathDocument(xreader);
                    foreach (XPathNavigator node in doc.CreateNavigator().Select("/graphml/graph/node"))
                    {
                        string id = node.GetAttribute("id", "");
                        Assert.IsTrue(vertices.ContainsKey(id));
                    }
                    TestConsole.Write(", vertices ok");

                    // check all edges are loaded
                    foreach (XPathNavigator node in doc.CreateNavigator().Select("/graphml/graph/edge"))
                    {
                        string source = node.GetAttribute("source", "");
                        string target = node.GetAttribute("target", "");
                        Assert.IsTrue(g.ContainsEdge(vertices[source], vertices[target]));
                    }
                    TestConsole.Write(", edges ok");
                }
                TestConsole.WriteLine();
            }
        }
    }


}
