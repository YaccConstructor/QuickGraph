using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph;
using System.IO;
using System.Xml;
using QuickGraph.Algorithms.Search;

namespace QuickGraph.Heap
{
    public sealed class GcTypeHeap
    {
        private readonly BidirectionalGraph<GcType, GcTypeEdge> graph;

        internal GcTypeHeap(BidirectionalGraph<GcType, GcTypeEdge> graph)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");

            this.graph = graph;
        }

        public static GcTypeHeap Load(string heapXmlFileName)
        {
            if (String.IsNullOrEmpty(heapXmlFileName))
                throw new ArgumentNullException("heapXmlFileName");
            if (!File.Exists(heapXmlFileName))
                throw new FileNotFoundException(heapXmlFileName);

            using (XmlReader reader = XmlReader.Create(heapXmlFileName))
            {
                GcTypeGraphReader greader = new GcTypeGraphReader();
                greader.Read(reader);
                return new GcTypeHeap(greader.Graph).RemoveDefault(); ;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} types", this.graph.VertexCount);
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
