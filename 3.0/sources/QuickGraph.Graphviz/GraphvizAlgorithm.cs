using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public sealed class GraphvizAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly static Regex writeLineReplace = new Regex("\n", RegexOptions.Compiled | RegexOptions.Multiline);
        private IVertexAndEdgeSet<TVertex, TEdge> visitedGraph;
        private StringWriter output;
        private GraphvizImageType imageType;
        private readonly Dictionary<TVertex, int> vertexIds = new Dictionary<TVertex, int>();

        private GraphvizGraph graphFormat;
        private GraphvizVertex commonVertexFormat;
        private GraphvizEdge commonEdgeFormat;

        public GraphvizAlgorithm(IVertexAndEdgeSet<TVertex, TEdge> g)
            :this(g,".",GraphvizImageType.Png)
        {}

        public GraphvizAlgorithm(
            IVertexAndEdgeSet<TVertex, TEdge> g,
            String path,
            GraphvizImageType imageType
            )
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (path == null)
                throw new ArgumentNullException("path");
            this.visitedGraph = g;
            this.imageType = imageType;
            this.graphFormat = new GraphvizGraph();
            this.commonVertexFormat = new GraphvizVertex();
            this.commonEdgeFormat = new GraphvizEdge();
        }

        public string Escape(string value)
        {
            return writeLineReplace.Replace(value, "\\n");
        }

        public GraphvizGraph GraphFormat
        {
            get
            {
                return graphFormat;
            }
        }

        public GraphvizVertex CommonVertexFormat
        {
            get
            {
                return commonVertexFormat;
            }
        }

        public GraphvizEdge CommonEdgeFormat
        {
            get
            {
                return commonEdgeFormat;
            }
        }

        public IVertexAndEdgeSet<TVertex, TEdge> VisitedGraph
        {
            get
            {
                return visitedGraph;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("graph");
                visitedGraph = value;
            }
        }

        /// <summary>
        /// Dot output stream.
        /// </summary>
        public StringWriter Output
        {
            get
            {
                return output;
            }
        }

        /// <summary>
        /// Current image output type
        /// </summary>
        public GraphvizImageType ImageType
        {
            get
            {
                return imageType;
            }
            set
            {
                imageType = value;
            }
        }
/*
        /// <summary>
        /// Event raised while drawing a cluster
        /// </summary>
        public event FormatClusterEventHandler<Vertex,Edge> FormatCluster;
        private void OnFormatCluster(IVertexAndEdgeListGraph<Vertex,Edge> cluster)
        {
            if (FormatCluster != null)
            {
                FormatClusterEventArgs<Vertex,Edge> args =
                    new FormatClusterEventArgs<Vertex,Edge>(cluster, new GraphvizGraph());
                FormatCluster(this, args);
                string s = args.GraphFormat.ToDot();
                if (s.Length != 0)
                    Output.WriteLine(s);
            }
        }
*/
        public event FormatVertexEventHandler<TVertex> FormatVertex;
        private void OnFormatVertex(TVertex v)
        {
            Output.Write("{0} ", this.vertexIds[v]);
            if (FormatVertex != null)
            {
                GraphvizVertex gv = new GraphvizVertex();
                gv.Label = v.ToString();
                FormatVertex(this, new FormatVertexEventArgs<TVertex>(gv, v));

                string s = gv.ToDot();
                if (s.Length != 0)
                    Output.Write("[{0}]", s);
            }
            Output.WriteLine(";");
        }

        public event FormatEdgeEventHandler<TVertex,TEdge> FormatEdge;
        private void OnFormatEdge(TEdge e)
        {
            if (FormatEdge != null)
            {
                GraphvizEdge ev = new GraphvizEdge();
                FormatEdge(this, new FormatEdgeEventArgs<TVertex,TEdge>(ev, e));
                Output.Write(" {0}", ev.ToDot());
            }
        }

        public string Generate(IDotEngine dot, string outputFileName)
        {
            if (dot == null)
                throw new ArgumentNullException("dot");
            if (outputFileName == null)
                throw new ArgumentNullException("outputFileName");

            this.vertexIds.Clear();

            this.output = new StringWriter();

            // build vertex id map
            int i=0;
            foreach(TVertex v in this.VisitedGraph.Vertices)
                this.vertexIds.Add(v,i++);

            Output.WriteLine("digraph G {");

            String gf = GraphFormat.ToDot();
            if (gf.Length > 0)
                Output.WriteLine(gf);
            String vf = CommonVertexFormat.ToDot();
            if (vf.Length > 0)
                Output.WriteLine("node [{0}];", vf);
            String ef = CommonEdgeFormat.ToDot();
            if (ef.Length > 0)
                Output.WriteLine("edge [{0}];", ef);

            // initialize vertex map
            IDictionary<TVertex,GraphColor> colors = new Dictionary<TVertex,GraphColor>();
            foreach (var v in VisitedGraph.Vertices)
                colors[v] = GraphColor.White;
            IDictionary<TEdge, GraphColor> edgeColors = new Dictionary<TEdge, GraphColor>();
            foreach (var e in VisitedGraph.Edges)
                edgeColors[e] = GraphColor.White;

            WriteVertices(colors, VisitedGraph.Vertices);
            WriteEdges(edgeColors, VisitedGraph.Edges);

            Output.WriteLine("}");

            return dot.Run(ImageType, Output.ToString(), outputFileName);
        }

        private void WriteVertices(
            IDictionary<TVertex,GraphColor> colors,
            IEnumerable<TVertex> vertices)
        {
            foreach (var v in vertices)
            {
                if (colors[v] == GraphColor.White)
                {
                    OnFormatVertex(v);
                    colors[v] = GraphColor.Black;
                }
            }
        }

        private void WriteEdges(
            IDictionary<TEdge,GraphColor> edgeColors,
            IEnumerable<TEdge> edges)
        {
            foreach (var e in edges)
            {
                if (edgeColors[e] != GraphColor.White)
                    continue;

                Output.Write("{0} -> {1} [",
                    this.vertexIds[e.Source],
                    this.vertexIds[e.Target]
                    );

                OnFormatEdge(e);
                Output.WriteLine("];");

                edgeColors[e] = GraphColor.Black;
            }
        }
    }
}
