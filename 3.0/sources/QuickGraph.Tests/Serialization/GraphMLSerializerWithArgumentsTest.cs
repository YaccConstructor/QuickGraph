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
                float _float,
                double _double,
                bool _bool
                )
            {
                this.id = id;
                this.Int = _int;
                this.Long = _long;
                this.Bool = _bool;
                this.Float = _float;
                this.Double = _double;
                this.String = _string;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("v_string")]
            public string String { get; set; }
            [XmlAttribute("v_int")]
            public int Int {get;set;}
            [XmlAttribute("v_long")]
            public long Long {get;set;}
            [XmlAttribute("v_bool")]
            public bool Bool { get; set; }
            [XmlAttribute("v_float")]
            public float Float { get; set; }
            [XmlAttribute("v_double")]
            public double Double { get; set; }
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
                float _float,
                double _double,
                bool _bool
                )
                : base(source, target)
            {
                this.id = id;
                this.String = _string;
                this.Int = _int;
                this.Long = _long;
                this.Float = _float;
                this.Double = _double;
                this.Bool = _bool;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("e_string")]
            public string String  {get;set;}
            [XmlAttribute("e_int")]
            public int Int { get; set; }
            [XmlAttribute("e_long")]
            public long Long { get; set; }
            [XmlAttribute("e_double")]
            public double Double { get; set; }
            [XmlAttribute("e_bool")]
            public bool Bool { get; set; }
            [XmlAttribute("v_float")]
            public float Float { get; set; }
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
                10,
                20,
                25.0F,
                30.0,
                true
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
                    "vertex",
                    10,
                    20,
                    25.0F,
                    30.0,
                    true
                    );
                TestVertex v2 = new TestVertex(
                    "v2",
                    "vertex2",
                    50,
                    60,
                    25.0F,
                    70.0,
                    false
                    );

                g.AddVertex(v1);
                g.AddVertex(v2);

                TestEdge edge = new TestEdge(
                    v1,v2,
                    "e1",
                    "edge",
                    90,
                    100,
                    25.0F,
                    110.0,
                    true
                    );
                g.AddEdge(edge);
                VerifySerialization(g);
            }
        }

        public class Dummy { }
    }
}
