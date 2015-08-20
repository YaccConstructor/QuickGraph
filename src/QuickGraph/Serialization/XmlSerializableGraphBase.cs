using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace QuickGraph.Serialization
{
    public class XmlSerializableEdge<TVertex>
        : IEdge<TVertex>
    {
        [XmlElement]
        public TVertex Source { get; set; }

        [XmlElement]
        public TVertex Target { get; set; }
    }

    /// <summary>
    /// A base class that creates a proxy to a graph that is xml serializable
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    /// <typeparam name="TGraph"></typeparam>
    [XmlRoot("graph")]
    public class XmlSerializableGraph<TVertex, TEdge, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IMutableVertexAndEdgeListGraph<TVertex, TEdge>, new()
    {
        readonly TGraph graph;
        XmlEdgeList _edges;

        public XmlSerializableGraph(TGraph graph)
        {
            Contract.Requires(graph != null);

            this.graph = graph;
        }

        public XmlSerializableGraph()
            : this(new TGraph())
        { }

        [XmlElement("graph-traits")]
        public TGraph Graph
        {
            get { return this.graph; }
        }

        [XmlArray("edges")]
        [XmlArrayItem("edge")]
        public IEnumerable<TEdge> Edges
        {
            get
            {
                if (this._edges == null)
                    this._edges = new XmlEdgeList(this.graph);
                return this._edges;
            }
        }

        public class XmlEdgeList
            : IEnumerable<TEdge>
        {
            readonly TGraph graph;

            internal XmlEdgeList(TGraph graph)
            {
                Contract.Requires(graph != null);

                this.graph = graph;
            }

            public IEnumerator<TEdge> GetEnumerator()
            {
                return this.graph.Edges.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public void Add(TEdge edge)
            {
                Contract.Requires(edge != null);

                this.graph.AddVerticesAndEdge(edge);
            }
        }
    }
}
