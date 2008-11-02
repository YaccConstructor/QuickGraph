using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Serialization
{
    [TestFixture, PexClass]
    [CurrentFixture]
    public partial class GraphMLSerializerWithArgumentsTest
    {
        public sealed class TestVertex : IIdentifiable
        {
            private string id;

            public TestVertex(
                string id,
                string _string,
                int _int,
                long _long,
                double _double,
                float _float,
                bool _bool,
                object _object
                )
            {
                this.id = id;
                this.String = _string;
                this.Int = _int;
                this.Long = _long;
                //this.Float = _float;
                //this.Double = _double;
                //this.Bool = _bool;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("string")]
            public string String { get; set; }
            [XmlAttribute("int")]
            public int Int {get;set;}
            [XmlAttribute("long")]
            public long Long {get;set;}
        }

        public sealed class TestEdge : Edge<TestVertex>, IIdentifiable
        {
            private string id;

            public TestEdge(
                TestVertex source,
                TestVertex target,
                string id,
                string _string,
                int _int,
                long _long,
                double _double,
                float _float,
                bool _bool,
                object _object)
                : base(source, target)
            {
                this.id = id;
                this.String = _string;
                this.Int = _int;
                this.Long = _long;
                this.Double = _double;
                this.Bool = _bool;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("p_string")]
            public string String  {get;set;}
            [XmlAttribute("p_int")]
            public int Int { get; set; }
            [XmlAttribute("p_long")]
            public long Long { get; set; }
            [XmlAttribute("p_double")]
            public double Double { get; set; }
            [XmlAttribute("p_bool")]
            public bool Bool { get; set; }
            [XmlAttribute("p_decimal")]
            public decimal Decimal { get; set; }
        }

        public sealed class TestAdjacencyGraph : AdjacencyGraph<TestVertex, TestEdge>
        { }

        [Test]
        [Repeat(2)]
        public void WriteVertex()
        {
            TestAdjacencyGraph g = new TestAdjacencyGraph();
            TestVertex v = new TestVertex(
                "v1",
                "string",
                1,
                2,
                3.0,
                4.0F,
                true,
                new Dummy()
                );

            g.AddVertex(v);
            VerifySerialization(g);
        }

        private void VerifySerialization(TestAdjacencyGraph g)
        {
            GraphMLSerializer<TestVertex, TestEdge> serializer = new GraphMLSerializer<TestVertex, TestEdge>();

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, g);
                String xml = writer.ToString();
                Console.WriteLine(xml);
                XmlAssert.IsWellFormedXml(xml);
            }
        }

        [Test]
        [Repeat(2)]
        public void WriteEdge()
        {
            {
                TestAdjacencyGraph g = new TestAdjacencyGraph();
                TestVertex v1 = new TestVertex(
                    "v1",
                    "string",
                    1,
                    2,
                    3.0,
                    4.0F,
                    true,
                    new Dummy()
                    );
                TestVertex v2 = new TestVertex(
                    "v2",
                    "string2",
                    5,
                    6,
                    7.0,
                    8.0F,
                    true,
                    new Dummy()
                    );

                g.AddVertex(v1);
                g.AddVertex(v2);

                TestEdge edge = new TestEdge(
                    v1,v2,
                    "e1",
                    "string",
                    9,
                    10,
                    11.0,
                    12.0F,
                    true,
                    new Dummy()
                    );
                g.AddEdge(edge);
                VerifySerialization(g);
            }
        }

        public class Dummy { }
    }
}
