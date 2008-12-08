using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Pex.Framework;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Serialization
{
    [TestClass, PexClass]
    public partial class GraphMLSerializerWithArgumentsTest
    {
        public sealed class TestVertex : IIdentifiable
        {
            private string id;

            public TestVertex(
                string id)
            {
                this.id = id;
            }

            public TestVertex(
                string id,
                string _string,
                int _int,
                long _long,
                float _float,
                double _double,
                bool _bool
                )
                :this(id)
            {
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

            string _string;
            [XmlAttribute("v_string")]
            public string String
            {
                get 
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._string; 
                }
                set 
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._string = value;
                }
            }
            int _int;
            [XmlAttribute("v_int")]
            public int Int
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._int;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._int = value;
                }
            }
            long _long;
            [XmlAttribute("v_long")]
            public long Long
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._long;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._long = value;
                }
            }

            bool _bool;
            [XmlAttribute("v_bool")]
            public bool Bool
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._bool;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._bool = value;
                }
            }

            float _float;
            [XmlAttribute("v_float")]
            public float Float
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._float;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._float = value;
                }
            }

            double _double;
            [XmlAttribute("v_double")]
            public double Double
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._double;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._double = value;
                }
            }
        }

        public sealed class TestEdge : Edge<TestVertex>, IIdentifiable
        {
            private string id;

            public TestEdge(
                TestVertex source,
                TestVertex target,
                string id)
                :base(source, target)
            {
                this.id = id;
            }

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
                : this(source, target, id)
            {
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
            [XmlAttribute("e_float")]
            public float Float { get; set; }
        }

        public sealed class TestAdjacencyGraph : AdjacencyGraph<TestVertex, TestEdge>
        { }

        [TestMethod]
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

            string xml;
            using (var writer = new StringWriter())
            {
                using (var xwriter = XmlWriter.Create(writer))
                    g.SerializeToGraphML(xwriter);

                xml = writer.ToString();
            }

            TestAdjacencyGraph newg;
            using (var reader = new StringReader(xml))
            {
                newg = new TestAdjacencyGraph();
                newg.DeserializeAndValidateFromGraphML(
                    reader,
                    id => new TestVertex(id),
                    (source, target, id) => new TestEdge(source, target, id)
                    );
            }

            string newxml;
            using (var writer = new StringWriter())
            {
                using (var xwriter = XmlWriter.Create(writer))
                    serializer.Serialize(xwriter, newg);
                newxml = writer.ToString();
            }

            Assert.AreEqual(xml, newxml);
        }

        [TestMethod]
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
