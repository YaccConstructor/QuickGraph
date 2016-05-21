using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    /// <summary>
    /// An algorithm that renders a graph to the Graphviz DOT format.
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public class GraphvizAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly static Regex writeLineReplace = new Regex("\n", RegexOptions.Compiled | RegexOptions.Multiline);
        private IEdgeListGraph<TVertex, TEdge> visitedGraph;
        private StringWriter output;
        private GraphvizImageType imageType;
        private readonly Dictionary<TVertex, int> vertexIds = new Dictionary<TVertex, int>();
        private int clusterCount;
        private GraphvizGraph graphFormat;
        private GraphvizVertex commonVertexFormat;
        private GraphvizEdge commonEdgeFormat;

        public GraphvizAlgorithm(IEdgeListGraph<TVertex, TEdge> g)
            :this(g,".",GraphvizImageType.Png)
        {}

        public GraphvizAlgorithm(
            IEdgeListGraph<TVertex, TEdge> g,
            String path,
            GraphvizImageType imageType
            )
        {
            Contract.Requires(g != null);
            Contract.Requires(!String.IsNullOrEmpty(path));

            this.clusterCount = 0;
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

        public IEdgeListGraph<TVertex, TEdge> VisitedGraph
        {
            get
            {
                return visitedGraph;
            }
            set
            {
                Contract.Requires(value != null);

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

        internal int ClusterCount
        {
            get
            {
                return clusterCount;
            }
            set
            {
                clusterCount = value;
            }
        }


        public event FormatClusterEventHandler<TVertex,TEdge> FormatCluster;
        private void OnFormatCluster(IVertexAndEdgeListGraph<TVertex,TEdge> cluster)
        {
            if (FormatCluster != null)
            {
                FormatClusterEventArgs<TVertex,TEdge> args =
                    new FormatClusterEventArgs<TVertex,TEdge>(cluster, new GraphvizGraph());
                FormatCluster(this, args);
                string s = args.GraphFormat.ToDot();
                if (s.Length != 0)
                    Output.WriteLine(s);
            }
      }

        public event FormatVertexEventHandler<TVertex> FormatVertex;
        private void OnFormatVertex(TVertex v)
        {
            Output.Write("{0} ", this.vertexIds[v]);
            if (FormatVertex != null)
            {
                var gv = new GraphvizVertex();
                gv.Label = v.ToString();
                FormatVertex(this, new FormatVertexEventArgs<TVertex>(v, gv));

                string s = gv.ToDot();
                if (s.Length != 0)
                    Output.Write("[{0}]", s);
            }
            Output.WriteLine(";");
        }

        public event FormatEdgeAction<TVertex,TEdge> FormatEdge;
        private void OnFormatEdge(TEdge e)
        {
            if (FormatEdge != null)
            {
                var ev = new GraphvizEdge();
                FormatEdge(this, new FormatEdgeEventArgs<TVertex,TEdge>(e, ev));
                Output.Write(" {0}", ev.ToDot());
            }
        }

        public string Generate()
        {
            ClusterCount = 0;
            this.vertexIds.Clear();
            this.output = new StringWriter();
            // build vertex id map
            int i = 0;
            foreach (TVertex v in this.VisitedGraph.Vertices)
                this.vertexIds.Add(v, i++);

            if (this.VisitedGraph.IsDirected)
                this.Output.Write("digraph ");
            else
                this.Output.Write("graph ");
            this.Output.Write(this.GraphFormat.Name);
            this.Output.WriteLine(" {");

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
            var colors = new Dictionary<TVertex, GraphColor>();
            foreach (var v in VisitedGraph.Vertices)
                colors[v] = GraphColor.White;
            var edgeColors = new Dictionary<TEdge, GraphColor>();
            foreach (var e in VisitedGraph.Edges)
                edgeColors[e] = GraphColor.White;

            if (VisitedGraph is IClusteredGraph)
                WriteClusters(colors, edgeColors, VisitedGraph as IClusteredGraph);

            WriteVertices(colors, VisitedGraph.Vertices);
            WriteEdges(edgeColors, VisitedGraph.Edges);

            Output.WriteLine("}");
            return Output.ToString();
        }

        public string Generate(IDotEngine dot, string outputFileName)
        {
            Contract.Requires(dot != null);
            Contract.Requires(!String.IsNullOrEmpty(outputFileName));

            var output = this.Generate();
            return dot.Run(ImageType, Output.ToString(), outputFileName);
        }
        internal void WriteClusters (
        IDictionary<TVertex, GraphColor> colors,
        IDictionary<TEdge, GraphColor> edgeColors,
        IClusteredGraph parent
    ) 
        {
            ++ClusterCount;
            foreach (IVertexAndEdgeListGraph<TVertex,TEdge> g in parent.Clusters)
            {
                Output.Write("subgraph cluster{0}", ClusterCount.ToString());
                Output.WriteLine(" {");
                OnFormatCluster(g);
                if (g is IClusteredGraph)
                    WriteClusters(colors, edgeColors, g as IClusteredGraph);
                if (parent.Colapsed)
                {
                    foreach (TVertex v in g.Vertices)
                    {
                        colors[v] = GraphColor.Black;
                    }
                    foreach (TEdge e in g.Edges)
                        edgeColors[e] = GraphColor.Black;
                }
                else
                {
                    WriteVertices(colors, g.Vertices);
                    WriteEdges(edgeColors, g.Edges);
                }
                Output.WriteLine("}");
            }
        }

        private void WriteVertices(
            IDictionary<TVertex,GraphColor> colors,
            IEnumerable<TVertex> vertices)
        {
            Contract.Requires(colors != null);
            Contract.Requires(vertices != null);

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
            Contract.Requires(edgeColors != null);
            Contract.Requires(edges != null);

            foreach (var e in edges)
            {
                if (edgeColors[e] != GraphColor.White)
                    continue;
                if (this.VisitedGraph.IsDirected)
                    Output.Write("{0} -> {1} [",
                        this.vertexIds[e.Source],
                        this.vertexIds[e.Target]
                    );
                else
                    Output.Write("{0} -- {1} [",
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
