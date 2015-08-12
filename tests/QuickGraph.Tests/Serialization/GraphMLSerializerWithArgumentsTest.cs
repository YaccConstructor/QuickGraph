using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Pex.Framework;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Linq;

namespace QuickGraph.Serialization
{
    [TestClass, PexClass]
    public partial class GraphMLSerializerWithArgumentsTest
    {
        public sealed class TestGraph
            : AdjacencyGraph<TestVertex, TestEdge>
        {
            string _string;
            [XmlAttribute("g_string")]
            [DefaultValue("bla")]
            public string String
            {
                get 
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._string; 
                }
                set 
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._string = value;
                }
            }
            int _int;
            [XmlAttribute("g_int")]
            [DefaultValue(1)]
            public int Int
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._int;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._int = value;
                }
            }
            long _long;
            [XmlAttribute("g_long")]
            [DefaultValue(2L)]
            public long Long
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._long;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._long = value;
                }
            }

            bool _bool;
            [XmlAttribute("g_bool")]
            public bool Bool
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._bool;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._bool = value;
                }
            }

            float _float;
            [XmlAttribute("g_float")]
            public float Float
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._float;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._float = value;
                }
            }

            double _double;
            [XmlAttribute("g_double")]
            public double Double
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._double;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._double = value;
                }
            }
        }

        public sealed class TestVertex
        {
            private string id;

            public TestVertex(
                string id)
            {
                this.id = id;
            }

            public string ID
            {
                get { return this.id; }
            }

            string _stringd;
            [XmlAttribute("v_stringdefault")]
            [DefaultValue("bla")]
            public string StringDefault
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._stringd;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._stringd = value;
                }
            }

            string _string;
            [XmlAttribute("v_string")]
            [DefaultValue("bla")]
            public string String
            {
                get 
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._string; 
                }
                set 
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._string = value;
                }
            }
            int _int;
            [XmlAttribute("v_int")]
            [DefaultValue(1)]
            public int Int
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._int;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._int = value;
                }
            }
            long _long;
            [XmlAttribute("v_long")]
            [DefaultValue(2L)]
            public long Long
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._long;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._long = value;
                }
            }

            bool _bool;
            [XmlAttribute("v_bool")]
            public bool Bool
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._bool;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._bool = value;
                }
            }

            float _float;
            [XmlAttribute("v_float")]
            public float Float
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._float;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._float = value;
                }
            }

            double _double;
            [XmlAttribute("v_double")]
            public double Double
            {
                get
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._double;
                }
                set
                {
                    TestConsole.WriteLine(MethodInfo.GetCurrentMethod());
                    this._double = value;
                }
            }
        }

        public sealed class TestEdge : Edge<TestVertex>
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
            [DefaultValue("bla")]
            public string String  {get;set;}
            [XmlAttribute("e_int")]
            [DefaultValue(1)]
            public int Int { get; set; }
            [XmlAttribute("e_long")]
            [DefaultValue(2L)]
            public long Long { get; set; }
            [XmlAttribute("e_double")]
            public double Double { get; set; }
            [XmlAttribute("e_bool")]
            public bool Bool { get; set; }
            [XmlAttribute("e_float")]
            public float Float { get; set; }
        }

        [TestMethod]
        public void WriteVertex()
        {
            TestGraph g = new TestGraph()
            {
                Bool = true,
                Double = 1.0,
                Float = 2.0F,
                Int = 10,
                Long = 100,
                String = "foo"
            };

            TestVertex v = new TestVertex("v1")
            {
                StringDefault = "bla",
                String = "string",
                Int = 10,
                Long = 20,
                Float = 25.0F,
                Double = 30.0,
                Bool = true
            };

            g.AddVertex(v);
            var sg = VerifySerialization(g);
            Assert.AreEqual(g.Bool, sg.Bool);
            Assert.AreEqual(g.Double, sg.Double);
            Assert.AreEqual(g.Float, sg.Float);
            Assert.AreEqual(g.Int, sg.Int);
            Assert.AreEqual(g.Long, sg.Long);
            Assert.AreEqual(g.String, sg.String);

            var sv = Enumerable.First(sg.Vertices);
            Assert.AreEqual(sv.StringDefault, "bla");
            Assert.AreEqual(v.String, sv.String);
            Assert.AreEqual(v.Int, sv.Int);
            Assert.AreEqual(v.Long, sv.Long);
            Assert.AreEqual(v.Float, sv.Float);
            Assert.AreEqual(v.Double, sv.Double);
            Assert.AreEqual(v.Bool, sv.Bool);
        }

        private TestGraph VerifySerialization(TestGraph g)
        {
            string xml;
            using (var writer = new StringWriter())
            {
                var settins = new XmlWriterSettings();
                settins.Indent = true;
                using (var xwriter = XmlWriter.Create(writer, settins))
                    g.SerializeToGraphML<TestVertex, TestEdge, TestGraph>(
                        xwriter,
                        v => v.ID,
                        e => e.ID
                        );

                xml = writer.ToString();
                TestConsole.WriteLine("serialized: " + xml);
            }

            TestGraph newg;
            using (var reader = new StringReader(xml))
            {
                newg = new TestGraph();
                newg.DeserializeAndValidateFromGraphML(
                    reader,
                    id => new TestVertex(id),
                    (source, target, id) => new TestEdge(source, target, id)
                    );
            }

            string newxml;
            using (var writer = new StringWriter())
            {
                var settins = new XmlWriterSettings();
                settins.Indent = true;
                using (var xwriter = XmlWriter.Create(writer, settins))
                    newg.SerializeToGraphML<TestVertex, TestEdge, TestGraph>(
                        xwriter,
                        v => v.ID,
                        e => e.ID);
                newxml = writer.ToString();
                TestConsole.WriteLine("roundtrip: " + newxml);
            }

            Assert.AreEqual(xml, newxml);

            return newg;
        }

        [TestMethod]
        public void WriteEdge()
        {
            {
                var g = new TestGraph()
                {
                    Bool = true,
                    Double = 1.0,
                    Float = 2.0F,
                    Int = 10,
                    Long = 100,
                    String = "foo"
                };

                TestVertex v1 = new TestVertex("v1")
                {
                    StringDefault = "bla",
                    String = "string",
                    Int = 10,
                    Long = 20,
                    Float = 25.0F,
                    Double = 30.0,
                    Bool = true
                };

                TestVertex v2 = new TestVertex("v2")
                {
                    StringDefault = "bla",
                    String = "string2",
                    Int = 110,
                    Long = 120,
                    Float = 125.0F,
                    Double = 130.0,
                    Bool = true
                };


                g.AddVertex(v1);
                g.AddVertex(v2);

                var e = new TestEdge(
                    v1,v2,
                    "e1",
                    "edge",
                    90,
                    100,
                    25.0F,
                    110.0,
                    true
                    );
                g.AddEdge(e);
                var sg = VerifySerialization(g);

                var se = Enumerable.First(sg.Edges);
                Assert.AreEqual(e.ID, se.ID);
                Assert.AreEqual(e.String, se.String);
                Assert.AreEqual(e.Int, se.Int);
                Assert.AreEqual(e.Long, se.Long);
                Assert.AreEqual(e.Float, se.Float);
                Assert.AreEqual(e.Double, se.Double);
                Assert.AreEqual(e.Bool, se.Bool);

            }
        }

        public class Dummy { }
    }
}
