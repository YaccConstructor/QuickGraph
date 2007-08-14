using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph;
using System.IO;
using System.Xml;
using QuickGraph.Algorithms.Search;
using QuickGraph.Glee;
using Microsoft.Glee.GraphViewerGdi;

namespace QuickGraph.Heap
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://www.cs.utexas.edu/users/speedway/DaCapo/papers/cork-popl-2007.pdf
    /// </remarks>
    public sealed class GcTypeHeap
    {
        private readonly BidirectionalGraph<GcType, GcTypeEdge> graph;
        private int size = -1;
        private GViewer viewer;

        internal GcTypeHeap(BidirectionalGraph<GcType, GcTypeEdge> graph)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");

            this.graph = graph;
        }

        public int Size
        {
            get 
            {
                if (this.size < 0)
                {
                    int s = 0;
                    foreach (GcType type in this.graph.Vertices)
                        s += type.Size;
                    this.size = s;
                }
                return this.size; 
            }
        }

        public static GcTypeHeap Load(string heapXmlFileName)
        {
            if (String.IsNullOrEmpty(heapXmlFileName))
                throw new ArgumentNullException("heapXmlFileName");
            if (!File.Exists(heapXmlFileName))
                throw new FileNotFoundException(heapXmlFileName);

            Console.WriteLine("loading {0}", heapXmlFileName);
            using (XmlReader reader = XmlReader.Create(heapXmlFileName))
            {
                GcTypeGraphReader greader = new GcTypeGraphReader();
                greader.Read(reader);
                GcTypeHeap heap = new GcTypeHeap(greader.Graph).RemoveDefault(); ;
                Console.WriteLine("heap: {0}", heap);
                return heap;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} types, {1} edges, {2}", 
                this.graph.VertexCount,
                this.graph.EdgeCount,
                FormatHelper.ToSize(this.Size));
        }

        public GcTypeCollection Types
        {
            get
            {
                return new GcTypeCollection(this.graph.Vertices).SortSize();
            }
        }

        public GcTypeHeap Clone()
        {
            return new GcTypeHeap(this.graph.Clone());
        }

        #region Rendering
        public GcTypeHeap Render()
        {
            Console.WriteLine("rendering...");
            if (this.graph.VertexCount > 500)
            {
                Console.WriteLine("too many vertices");
                return this;
            }

            GleeGraphPopulator<GcType, GcTypeEdge> populator = GleeGraphUtility.Create(this.graph);
            try
            {
                populator.NodeAdded += new GleeVertexNodeEventHandler<GcType>(populator_NodeAdded);
                populator.EdgeAdded += new GleeEdgeEventHandler<GcType, GcTypeEdge>(populator_EdgeAdded);
                populator.Compute();

                if (viewer == null)
                    viewer = new GViewer();
                viewer.Graph = populator.GleeGraph;
            }
            finally
            {
                populator.NodeAdded -= new GleeVertexNodeEventHandler<GcType>(populator_NodeAdded);
                populator.EdgeAdded -= new GleeEdgeEventHandler<GcType, GcTypeEdge>(populator_EdgeAdded);
            }

            return this;
        }

        void populator_EdgeAdded(object sender, GleeEdgeEventArgs<GcType, GcTypeEdge> e)
        {
            e.GEdge.Attr.Label = e.Edge.Count.ToString();
        }

        void populator_NodeAdded(object sender, GleeVertexEventArgs<GcType> args)
        {
            // root -> cirle
            if (args.Vertex.Root)
                args.Node.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
            else
                args.Node.Attr.Shape = Microsoft.Glee.Drawing.Shape.Box;

            // label
            args.Node.Attr.Label =
                String.Format("{0}\n{1} {2}",
                    args.Vertex.Name,
                    args.Vertex.Count,
                    FormatHelper.ToSize(args.Vertex.Size)
                    );
        }
        #endregion

        #region Filtering
        private GcTypeHeap RemoveDefault()
        {
            // remove default types
            this.graph.RemoveVertexIf(
                delegate(GcType type)
                {
                    switch (type.Name)
                    {
                        case "System.OutOfMemoryException":
                        case "System.StackOverflowException":
                        case "System.ExecutionEngineException":
                        case "System.RuntimeType":
                        case "System.Reflection.Missing":
                        case "System.IO.TextReader/SyncTextReader":
                        case "System.AppDomain":
                        case "System.AppDomainSetup":
                        case "System.IO.Stream/NullStream":
                        case "System.Text.UTF8Encoding":
                        case "System.__Filters":
                        case "System.Reflection.MemberFilter":
                        case "System.Text.CodePageEncoding":
                        case "System.IO.TextWriter/NullTextWriter":
                        case "System.Text.UTF8Encoding/UTF8Encoder":
                        case "System.IO.TextReader/NullTextReader":
                        case "System.IO.StreamReader/NullStreamReader":
                        case "System.Text.CodePageEncoding/Decoder":
                        case "System.IO.__ConsoleStream":
                        case "System.Text.Encoding/DefaultEncoder":
                        case "System.IO.TextWriter/SyncTextWriter":
                            return true;
                        default:
                            return false;
                    }
                });
            return this;
        }

        public GcTypeHeap Touching(string typeNames)
        {
            if (String.IsNullOrEmpty(typeNames))
                throw new ArgumentNullException("typeNames");

            return this.Clone().TouchingInPlace(typeNames);
        }

        private GcTypeHeap TouchingInPlace(string typeNames)
        {
            if (String.IsNullOrEmpty(typeNames))
                throw new ArgumentNullException("typeNames");

            IFilter filter = FilterHelper.ToFilter(typeNames);
            Console.WriteLine("filtering nodes not connected to type matching '{0}'", filter);
            Dictionary<GcType, GraphColor> colors = new Dictionary<GcType, GraphColor>(this.graph.VertexCount);
            foreach (GcType type in this.graph.Vertices)
                colors.Add(type, GraphColor.White);

            ReversedBidirectionalGraph<GcType, GcTypeEdge> rgraph = new ReversedBidirectionalGraph<GcType, GcTypeEdge>(graph);
            foreach (GcType type in this.graph.Vertices)
            {
                if (filter.Match(type.Name))
                {
                    { // parents
                        DepthFirstSearchAlgorithm<GcType, ReversedEdge<GcType, GcTypeEdge>> dfs =
                            new DepthFirstSearchAlgorithm<GcType, ReversedEdge<GcType, GcTypeEdge>>(rgraph, colors);
                        dfs.Visit(type, -1);
                    }
                    { // children
                        DepthFirstSearchAlgorithm<GcType, GcTypeEdge> dfs = new DepthFirstSearchAlgorithm<GcType, GcTypeEdge>(graph, colors);
                        dfs.Visit(type, -1);
                    }
                }
            }
            // remove all white vertices
            this.graph.RemoveVertexIf(delegate(GcType t)
            {
                return colors[t] == GraphColor.White;
            });
            Console.WriteLine("{0} types, {1} edges", graph.VertexCount, graph.EdgeCount);
            return this;
        }
        #endregion
    }
}
