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
    public partial class GraphMLSerializerWithArgumentsTest
    {
        public sealed class TestVertex : IIdentifiable
        {
            private string id;
            private string _string;
            private int _int;
            private long _long;
            private float _float;
            private double _double;
            private bool _bool;
            private object _object;

            public TestVertex(
                string id,
                string _string,
                int _int,
                long _long,
                double _double,
                float _float,
                bool _bool,
                object _object)
            {
                this.id = id;
                this._string = _string;
                this._int = _int;
                this._long = _long;
                this._float = _float;
                this._double = _double;
                this._bool = _bool;
                this._object = _object;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("string")]
            public string String
            {
                get { return this._string; }
            }
            [XmlAttribute("int")]
            public int Int
            {
                get { return this._int; }
            }
            [XmlAttribute("long")]
            public long Long
            {
                get { return this._long; }
            }
            [XmlAttribute("float")]
            public float Float
            {
                get { return this._float; }
            }
            [XmlAttribute("double")]
            public double Double
            {
                get { return this._double; }
            }
            [XmlAttribute("object")]
            public object Object
            {
                get { return this._object; }
            }
        }

        public sealed class TestEdge : Edge<TestVertex>, IIdentifiable
        {
            private string id;
            private string _string;
            private int _int;
            private long _long;
            private float _float;
            private double _double;
            private bool _bool;
            private object _object;

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
                this._string = _string;
                this._int = _int;
                this._long = _long;
                this._float = _float;
                this._double = _double;
                this._bool = _bool;
                this._object = _object;
            }

            public string ID
            {
                get { return this.id; }
            }

            [XmlAttribute("string")]
            public string String
            {
                get { return this._string; }
            }
            [XmlAttribute("int")]
            public int Int
            {
                get { return this._int; }
            }
            [XmlAttribute("long")]
            public long Long
            {
                get { return this._long; }
            }
            [XmlAttribute("float")]
            public float Float
            {
                get { return this._float; }
            }
            [XmlAttribute("double")]
            public double Double
            {
                get { return this._double; }
            }
            [XmlAttribute("object")]
            public object Object
            {
                get { return this._object; }
            }
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
