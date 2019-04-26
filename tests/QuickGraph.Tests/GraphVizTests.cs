using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Graphviz;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace QuickGraph.Tests
{
    [TestClass]
    public class GraphVizTests
    {
        private static CultureInfo oldCulture;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            oldCulture = Thread.CurrentThread.CurrentCulture;

            // Germany (de-DE) uses comma as decimal separator
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [TestMethod]
        public void CommonVertexFormat_FontSize_UseDecimalPointForFloats()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.CommonVertexFormat.Font = new Font(SystemFonts.DefaultFont.FontFamily, emSize: 1.75f);

            var res = gv.Generate();

            StringAssert.Contains(res, "fontsize=1.75", "Formatting floating points should always use dot as decimal separator");
        }

        [TestMethod]
        public void CommonVertexFormat_Z_UseDecimalPointForDoubles()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.CommonVertexFormat.Z = 1.75;

            var res = gv.Generate();

            StringAssert.Contains(res, "z=1.75", "Formatting floating points should always use dot as decimal separator");
        }

        [TestMethod]
        public void CommonEdgeFormat_FontSize_UseDecimalPointForFloats()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.CommonEdgeFormat.Font = new Font(SystemFonts.DefaultFont.FontFamily, emSize: 1.75f);

            var res = gv.Generate();

            StringAssert.Contains(res, "fontsize=1.75", "Formatting floating points should always use dot as decimal separator");
        }

        [TestMethod]
        public void CommonEdgeFormat_Weight_UseDecimalPointForDoubles()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.CommonEdgeFormat.Weight = 1.75;

            var res = gv.Generate();

            StringAssert.Contains(res, "weight=1.75", "Formatting floating points should always use dot as decimal separator");
        }

        [TestMethod]
        public void GraphFormat_RankSeparation_UseDecimalPointForDoubles()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.GraphFormat.RankSeparation = 0.75d;

            var res = gv.Generate();

            StringAssert.Contains(res, "ranksep=0.75", "Formatting floating points should always use dot as decimal separator");
        }

        [TestMethod]
        public void GraphFormat_FontSize_UseDecimalPointForFloats()
        {
            var graph = new AdjacencyGraph<string, IEdge<string>>(false);
            graph.AddVerticesAndEdge(new Edge<string>("s", "t"));

            var gv = new GraphvizAlgorithm<string, IEdge<string>>(graph);
            gv.GraphFormat.Font = new Font(SystemFonts.DefaultFont.FontFamily, emSize: 1.75f);

            var res = gv.Generate();

            StringAssert.Contains(res, "fontsize=1.75", "Formatting floating points should always use dot as decimal separator");
        }
    }
}
