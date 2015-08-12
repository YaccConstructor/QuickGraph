using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.XPath;
using System.Xml;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Serialization
{
    [TestClass]
    public class XmlSerializationTest
    {
        [TestMethod]
        public void DeserializeFromXml()
        {
            var doc = new XPathDocument("repro12273.xml");
            var ug = SerializationExtensions.DeserializeFromXml(doc,
                "graph", "node", "edge",
                nav => new UndirectedGraph<string, TaggedEdge<string, double>>(),
                nav => nav.GetAttribute("id", ""),
                nav => new TaggedEdge<string, double>(
                    nav.GetAttribute("source", ""),
                    nav.GetAttribute("target", ""),
                    int.Parse(nav.GetAttribute("weight", ""))
                    )
                );

            var ug2 = SerializationExtensions.DeserializeFromXml(
                XmlReader.Create("repro12273.xml"),
                "graph", "node", "edge", "",
                reader => new UndirectedGraph<string, TaggedEdge<string, double>>(),
                reader => reader.GetAttribute("id"),
                reader => new TaggedEdge<string, double>(
                    reader.GetAttribute("source"),
                    reader.GetAttribute("target"),
                    int.Parse(reader.GetAttribute("weight"))
                    )
                );

            Assert.AreEqual(ug.VertexCount, ug2.VertexCount);
            Assert.AreEqual(ug.EdgeCount, ug2.EdgeCount);
        }
    }
}
