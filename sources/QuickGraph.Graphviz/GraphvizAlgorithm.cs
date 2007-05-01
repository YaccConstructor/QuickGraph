using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NGraphviz;
using NGraphviz.Helpers;

namespace QuickGraph.Graphviz
{
    public sealed class GraphvizAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private static Regex writeLineReplace = new Regex("\n", RegexOptions.Compiled | RegexOptions.Multiline);
        private IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph;
        private StringWriter output;
        private Dot dot;
        private GraphvizImageType imageType;
        private Dictionary<Vertex, int> vertexIds = new Dictionary<Vertex, int>();

        private GraphvizGraph graphFormat;
        private GraphvizVertex commonVertexFormat;
        private GraphvizEdge commonEdgeFormat;

        public GraphvizAlgorithm(IVertexAndEdgeListGraph<Vertex, Edge> g)
            :this(g,".",GraphvizImageType.Png)
        {}

        public GraphvizAlgorithm(
            IVertexAndEdgeListGraph<Vertex,Edge> g,
            String path,
            GraphvizImageType imageType
            )
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (path == null)
                throw new ArgumentNullException("path");
            this.visitedGraph = g;
            this.dot = new Dot(path);
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

        public IVertexAndEdgeListGraph<Vertex,Edge> VisitedGraph
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
        /// Renderer
        /// </summary>
        public Dot Renderer
        {
            get
            {
                return dot;
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
        public event FormatVertexEventHandler<Vertex> FormatVertex;
        private void OnFormatVertex(Vertex v)
        {
            Output.Write("{0} ", this.vertexIds[v]);
            if (FormatVertex != null)
            {
                GraphvizVertex gv = new GraphvizVertex();
                gv.Label = v.ToString();
                FormatVertex(this, new FormatVertexEventArgs<Vertex>(gv, v));

                string s = gv.ToDot();
                if (s.Length != 0)
                    Output.Write("[{0}]", s);
            }
            Output.WriteLine(";");
        }

        public event FormatEdgeEventHandler<Vertex,Edge> FormatEdge;
        private void OnFormatEdge(Edge e)
        {
            if (FormatEdge != null)
            {
                GraphvizEdge ev = new GraphvizEdge();
                FormatEdge(this, new FormatEdgeEventArgs<Vertex,Edge>(ev, e));
                Output.Write(" {0}", ev.ToDot());
            }
        }

        public string Generate(string outputFileName)
        {
            if (outputFileName == null)
                throw new ArgumentNullException("outputFileName");

            this.vertexIds.Clear();

            this.output = new StringWriter();

            // build vertex id map
            int i=0;
            foreach(Vertex v in this.VisitedGraph.Vertices)
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
            IDictionary<Vertex,GraphColor> colors = new Dictionary<Vertex,GraphColor>();
            foreach (Vertex v in VisitedGraph.Vertices)
                colors[v] = GraphColor.White;
            IDictionary<Edge, GraphColor> edgeColors = new Dictionary<Edge, GraphColor>();
            foreach (Edge e in VisitedGraph.Edges)
                edgeColors[e] = GraphColor.White;

            // write
  //          if (VisitedGraph is IClusteredGraph)
//                WriteClusters(vertexColors, edgeColors, VisitedGraph as IClusteredGraph);

            WriteVertices(colors, VisitedGraph.Vertices);
            WriteEdges(edgeColors, VisitedGraph.Edges);

            Output.WriteLine("}");

            return dot.Run(ImageType, Output.ToString(), outputFileName);
        }
/*
        private void WriteClusters(
            VertexColorDictionary<Vertex,Edge> vertexColors,
            EdgeColorDictionary<Vertex,Edge> edgeColors,
            IClusteredGraph parent
            )
        {
            ++ClusterCount;
            foreach (IVertexAndEdgeListGraph g in parent.Clusters)
            {
                Output.Write("subgraph cluster{0}", ClusterCount.ToString());
                Output.WriteLine(" {");

                OnFormatCluster(g);

                if (g is IClusteredGraph)
                    WriteClusters(vertexColors, edgeColors, g as IClusteredGraph);

                if (parent.Colapsed)
                {
                    // draw cluster
                    // put vertices as black
                    foreach (IVertex v in g.Vertices)
                    {
                        vertexColors[v] = GraphColor.Black;

                    }
                    foreach (IEdge e in g.Edges)
                        edgeColors[e] = GraphColor.Black;

                    // add fake vertex

                }
                else
                {
                    WriteVertices(vertexColors, g.Vertices);
                    WriteEdges(edgeColors, g.Edges);
                }

                Output.WriteLine("}");
            }
        }
*/
        private void WriteVertices(
            IDictionary<Vertex,GraphColor> colors,
            IEnumerable<Vertex> vertices)
        {
            foreach (Vertex v in vertices)
            {
                if (colors[v] == GraphColor.White)
                {
                    OnFormatVertex(v);
                    colors[v] = GraphColor.Black;
                }
            }
        }

        private void WriteEdges(
            IDictionary<Edge,GraphColor> edgeColors,
            IEnumerable<Edge> edges)
        {
            foreach (Edge e in edges)
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
