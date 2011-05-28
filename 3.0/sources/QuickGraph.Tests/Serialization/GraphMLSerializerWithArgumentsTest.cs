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
            [XmlAttribute("g_int")]
            [DefaultValue(1)]
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
            [XmlAttribute("g_long")]
            [DefaultValue(2L)]
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
            [XmlAttribute("g_bool")]
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
            [XmlAttribute("g_float")]
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
            [XmlAttribute("g_double")]
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

            string[] _stringarray;
            [XmlAttribute("g_stringarray")]
            public string[] StringArray
            {
                get 
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._stringarray; 
                }
                set 
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._stringarray = value;
                }
            }
            int[] _intarray;
            [XmlAttribute("g_intarray")]
            public int[] IntArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._intarray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._intarray = value;
                }
            }
            long[] _longarray;
            [XmlAttribute("g_longarray")]
            public long[] LongArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._longarray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._longarray = value;
                }
            }

            bool[] _boolarray;
            [XmlAttribute("g_boolarray")]
            public bool[] BoolArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._boolarray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._boolarray = value;
                }
            }

            float[] _floatarray;
            [XmlAttribute("g_floatarray")]
            public float[] FloatArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._floatarray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._floatarray = value;
                }
            }

            double[] _doublearray;
            [XmlAttribute("g_doublearray")]
            public double[] DoubleArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._doublearray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._doublearray = value;
                }
            }

            [XmlAttribute("g_nullarray")]
            public int[] NullArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return null;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    Assert.IsNull(value);
                }
            }

            #region IList properties

            IList<string> _stringilist;
            [XmlAttribute("g_stringilist")]
            public IList<string> StringIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._stringilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._stringilist = value;
                }
            }
            IList<int> _intilist;
            [XmlAttribute("g_intilist")]
            public IList<int> IntIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._intilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._intilist = value;
                }
            }
            IList<long> _longilist;
            [XmlAttribute("g_longilist")]
            public IList<long> LongIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._longilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._longilist = value;
                }
            }

            IList<bool> _boolilist;
            [XmlAttribute("g_boolilist")]
            public IList<bool> BoolIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._boolilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._boolilist = value;
                }
            }

            IList<float> _floatilist;
            [XmlAttribute("g_floatilist")]
            public IList<float> FloatIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._floatilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._floatilist = value;
                }
            }

            IList<double> _doubleilist;
            [XmlAttribute("g_doubleilist")]
            public IList<double> DoubleIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._doubleilist;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._doubleilist = value;
                }
            }

            #endregion
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
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._stringd;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
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
            [DefaultValue(1)]
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
            [DefaultValue(2L)]
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

            int[] _intArray;
            [XmlAttribute("v_intarray")]
            public int[] IntArray
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._intArray;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._intArray = value;
                }
            }

            IList<int> _intIList;
            [XmlAttribute("v_intilist")]
            public IList<int> IntIList
            {
                get
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    return this._intIList;
                }
                set
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod());
                    this._intIList = value;
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

        private static readonly bool[] BoolArray = new bool[] { true, false, true, true };
        private static readonly int[] IntArray = new int[] { 2, 3, 45, 3, 44, -2, 3, 5, 99999999 };
        private static readonly long[] LongArray = new long[] { 3, 4, 43, 999999999999999999L, 445, 55, 3, 98, 49789238740598170, 987459, 97239, 234245, 0, -2232 };
        private static readonly float[] FloatArray = new float[] { 3.14159265F, 1.1F, 1, 23, -2, 987459, 97239, 234245, 0, -2232, 234.55345F };
        private static readonly double[] DoubleArray = new double[] { 3.14159265, 1.1, 1, 23, -2, 987459, 97239, 234245, 0, -2232, 234.55345 };
        private static readonly string[] StringArray = new string[] { "", "Quick", "", "brown", "fox", "jumps", "over", "the", "lazy", "dog", ".", "" };

        private static readonly IList<bool> BoolIList = new bool[] { true, false, true, true };
        private static readonly IList<int> IntIList = new int[] { 2, 3, 45, 3, 44, -2, 3, 5, 99999999 };
        private static readonly IList<long> LongIList = new long[] { 3, 4, 43, 999999999999999999L, 445, 55, 3, 98, 49789238740598170, 987459, 97239, 234245, 0, -2232 };
        private static readonly IList<float> FloatIList = new float[] { 3.14159265F, 1.1F, 1, 23, -2, 987459, 97239, 234245, 0, -2232, 234.55345F };
        private static readonly IList<double> DoubleIList = new double[] { 3.14159265, 1.1, 1, 23, -2, 987459, 97239, 234245, 0, -2232, 234.55345 };
        private static readonly IList<string> StringIList = new string[] { "", "Quick", "", "brown", "fox", "jumps", "over", "the", "lazy", "dog", ".", "" };
        
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
                String = "foo",
                BoolArray = BoolArray,
                IntArray = IntArray,
                LongArray = LongArray,
                FloatArray = FloatArray,
                DoubleArray = DoubleArray,
                StringArray = StringArray,
                NullArray = null,
                BoolIList = BoolIList,
                IntIList = IntIList,
                LongIList = LongIList,
                FloatIList = FloatIList,
                DoubleIList = DoubleIList,
                StringIList = StringIList,

            };

            TestVertex v = new TestVertex("v1")
            {
                StringDefault = "bla",
                String = "string",
                Int = 10,
                Long = 20,
                Float = 25.0F,
                Double = 30.0,
                Bool = true,
                IntArray = new int[] { 1, 2, 3, 4 },
                IntIList = new int[] { 4, 5, 6, 7 }
            };

            g.AddVertex(v);
            var sg = VerifySerialization(g);
            Assert.AreEqual(g.Bool, sg.Bool);
            Assert.AreEqual(g.Double, sg.Double);
            Assert.AreEqual(g.Float, sg.Float);
            Assert.AreEqual(g.Int, sg.Int);
            Assert.AreEqual(g.Long, sg.Long);
            Assert.AreEqual(g.String, sg.String);
            Assert.IsTrue(g.BoolArray.Equals1(sg.BoolArray));
            Assert.IsTrue(g.IntArray.Equals1(sg.IntArray));
            Assert.IsTrue(g.LongArray.Equals1(sg.LongArray));
            Assert.IsTrue(g.StringArray.Equals1(sg.StringArray));
            Assert.IsTrue(g.FloatArray.Equals1(sg.FloatArray, 0.001F));
            Assert.IsTrue(g.DoubleArray.Equals1(sg.DoubleArray, 0.0001));
            Assert.IsTrue(g.BoolIList.Equals1(sg.BoolIList));
            Assert.IsTrue(g.IntIList.Equals1(sg.IntIList));
            Assert.IsTrue(g.LongIList.Equals1(sg.LongIList));
            Assert.IsTrue(g.StringIList.Equals1(sg.StringIList));
            Assert.IsTrue(g.FloatIList.Equals1(sg.FloatIList, 0.001F));
            Assert.IsTrue(g.DoubleIList.Equals1(sg.DoubleIList, 0.0001));

            var sv = Enumerable.First(sg.Vertices);
            Assert.AreEqual(sv.StringDefault, "bla");
            Assert.AreEqual(v.String, sv.String);
            Assert.AreEqual(v.Int, sv.Int);
            Assert.AreEqual(v.Long, sv.Long);
            Assert.AreEqual(v.Float, sv.Float);
            Assert.AreEqual(v.Double, sv.Double);
            Assert.AreEqual(v.Bool, sv.Bool);
            Assert.IsTrue(v.IntArray.Equals1(sv.IntArray));
            Assert.IsTrue(v.IntIList.Equals1(sv.IntIList));
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
                Console.WriteLine("serialized: " + xml);
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
                Console.WriteLine("roundtrip: " + newxml);
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
