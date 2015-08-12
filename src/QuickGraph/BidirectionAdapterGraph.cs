using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// Wraps a vertex list graph (out-edges only) and caches the in-edge dictionary.
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class BidirectionAdapterGraph<TVertex, TEdge>
        : IBidirectionalGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IVertexAndEdgeListGraph<TVertex, TEdge> baseGraph;
        private readonly Dictionary<TVertex, EdgeList<TVertex, TEdge>> inEdges;

        public BidirectionAdapterGraph(IVertexAndEdgeListGraph<TVertex, TEdge> baseGraph)
        {
            Contract.Requires(baseGraph != null);

            this.baseGraph = baseGraph;
            this.inEdges = new Dictionary<TVertex, EdgeList<TVertex, TEdge>>(this.baseGraph.VertexCount);
            foreach (var edge in this.baseGraph.Edges)
            {
                EdgeList<TVertex, TEdge> list;
                if (!this.inEdges.TryGetValue(edge.Target, out list))
                    this.inEdges.Add(edge.Target, list = new EdgeList<TVertex, TEdge>());
                list.Add(edge);
            }
        }

        [Pure]
        public bool IsInEdgesEmpty(TVertex v)
        {
            return this.InDegree(v) == 0;
        }

        [Pure]
        public int InDegree(TVertex v)
        {
            EdgeList<TVertex, TEdge> edges;
            if (this.inEdges.TryGetValue(v, out edges))
                return edges.Count;
            else
                return 0;
        }

        static readonly TEdge[] emptyEdges = new TEdge[0];
        [Pure]
        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            EdgeList<TVertex, TEdge> edges;
            if (this.inEdges.TryGetValue(v, out edges))
                return edges;
            else
                return emptyEdges;
        }

        [Pure]
        public bool TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            EdgeList<TVertex, TEdge> es;
            if (this.inEdges.TryGetValue(v, out es))
            {
                edges = es;
                return true;
            }

            edges = null;
            return false;
        }

        [Pure]
        public TEdge InEdge(TVertex v, int index)
        {
            return this.inEdges[v][index];
        }

        [Pure]
        public int Degree(TVertex v)
        {
            return this.InDegree(v) + this.OutDegree(v);
        }

        [Pure]
        public bool ContainsEdge(TVertex source, TVertex target)
        {
            return this.baseGraph.ContainsEdge(source, target);
        }

        [Pure]
        public bool TryGetEdges(TVertex source, TVertex target, out IEnumerable<TEdge> edges)
        {
            return this.baseGraph.TryGetEdges(source, target, out edges);
        }

        [Pure]
        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            return this.baseGraph.TryGetEdge(source, target, out edge);
        }

        [Pure] // InterfacePureBug
        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.baseGraph.IsOutEdgesEmpty(v);
        }

        [Pure]
        public int OutDegree(TVertex v)
        {
            return this.baseGraph.OutDegree(v);
        }

        [Pure]
        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            return this.baseGraph.OutEdges(v);
        }

        [Pure]
        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            return this.baseGraph.TryGetOutEdges(v, out edges);
        }

        [Pure]
        public TEdge OutEdge(TVertex v, int index)
        {
            return this.baseGraph.OutEdge(v, index);
        }

        public bool IsDirected
        {
            get { return this.baseGraph.IsDirected; }
        }

        public bool AllowParallelEdges
        {
            get { return this.baseGraph.AllowParallelEdges; }
        }

        public bool IsVerticesEmpty
        {
            get { return this.baseGraph.IsVerticesEmpty; }
        }

        public int VertexCount
        {
            get { return this.baseGraph.VertexCount; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return this.baseGraph.Vertices; }
        }

        [Pure]
        public bool ContainsVertex(TVertex vertex)
        {
            return this.baseGraph.ContainsVertex(vertex);
        }

        public bool IsEdgesEmpty
        {
            get { return this.baseGraph.IsEdgesEmpty; }
        }

        public int EdgeCount
        {
            get { return this.baseGraph.EdgeCount; }
        }

        public virtual IEnumerable<TEdge> Edges
        {
            get { return this.baseGraph.Edges; }
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            return this.baseGraph.ContainsEdge(edge);
        }
    }
}
