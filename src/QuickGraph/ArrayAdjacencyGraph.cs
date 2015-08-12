using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An immutable directed graph data structure efficient for large sparse
    /// graph representation where out-edge need to be enumerated only.
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public sealed class ArrayAdjacencyGraph<TVertex, TEdge>
        : IVertexAndEdgeListGraph<TVertex, TEdge>
#if !SILVERLIGHT
        , ICloneable
#endif
        where TEdge : IEdge<TVertex>
    {
        readonly Dictionary<TVertex, TEdge[]> vertexOutEdges;
        readonly int edgeCount;

        public ArrayAdjacencyGraph(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
        {
            Contract.Requires(visitedGraph != null);
            this.vertexOutEdges = new Dictionary<TVertex, TEdge[]>(visitedGraph.VertexCount);
            this.edgeCount = visitedGraph.EdgeCount;
            foreach (var vertex in visitedGraph.Vertices)
            {
                var outEdges = new List<TEdge>(visitedGraph.OutEdges(vertex));
                this.vertexOutEdges.Add(vertex, outEdges.ToArray());
            }
        }

        private ArrayAdjacencyGraph(
            Dictionary<TVertex, TEdge[]> vertexOutEdges,
            int edgeCount
            )
        {
            Contract.Requires(vertexOutEdges != null);
            Contract.Requires(edgeCount >= 0);
            Contract.Requires(edgeCount == Enumerable.Sum(vertexOutEdges, kv => (kv.Value == null) ? 0 : kv.Value.Length));

            this.vertexOutEdges = vertexOutEdges;
            this.edgeCount = edgeCount;
        }

        #region IIncidenceGraph<TVertex,TEdge> Members
        public bool ContainsEdge(TVertex source, TVertex target)
        {
            TEdge edge;
            return this.TryGetEdge(source, target, out edge);
        }

        public bool TryGetEdges(TVertex source, TVertex target, out IEnumerable<TEdge> edges)
        {
            TEdge[] es;
            if (this.vertexOutEdges.TryGetValue(source, out es))
            {
                List<TEdge> _edges = null;
                for (int i = 0; i < es.Length; i++)
                {
                    if (es[i].Target.Equals(target))
                    {
                        if (_edges == null)
                            _edges = new List<TEdge>(es.Length - i);
                        _edges.Add(es[i]);
                    }
                }

                edges = _edges;
                return edges != null;
            }

            edges = null;
            return false;
        }

        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            TEdge[] edges;
            if (this.vertexOutEdges.TryGetValue(source, out edges) &&
                edges != null)
            {
                for (int i = 0; i < edges.Length; i++)
                {
                    if (edges[i].Target.Equals(target))
                    {
                        edge = edges[i];
                        return true;
                    }
                }
            }

            edge = default(TEdge);
            return false;
        }

        #endregion

        #region IImplicitGraph<TVertex,TEdge> Members
        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(TVertex v)
        {
            TEdge[] edges;
            if (this.vertexOutEdges.TryGetValue(v, out edges) &&
                edges != null)
                return edges.Length;
            return 0;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            TEdge[] edges;
            if (this.vertexOutEdges.TryGetValue(v, out edges) &&
                edges != null)
                return edges;

            return Enumerable.Empty<TEdge>();
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            TEdge[] aedges;
            if (this.vertexOutEdges.TryGetValue(v, out aedges) &&
                aedges != null)
            {
                edges = aedges;
                return true;
            }

            edges = null;
            return false;
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            return this.vertexOutEdges[v][index];
        }
        #endregion

        #region IGraph<TVertex,TEdge> Members
        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return true; }
        }        
        #endregion

        #region IImplicitVertexSet<TVertex> Members
        public bool ContainsVertex(TVertex vertex)
        {
            return this.vertexOutEdges.ContainsKey(vertex);
        }
        #endregion

        #region IVertexSet<TVertex> Members
        public bool IsVerticesEmpty
        {
            get { return this.vertexOutEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexOutEdges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                return this.vertexOutEdges.Keys;
            }
        }
        #endregion

        #region IEdgeSet<TVertex,TEdge> Members
        public bool IsEdgesEmpty
        {
            get { return this.edgeCount == 0; }
        }

        public int EdgeCount
        {
            get { return this.edgeCount; }
        }

        public IEnumerable<TEdge> Edges
        {
            get             
            { 
                foreach(var edges in this.vertexOutEdges.Values)
                    if (edges != null)
                        for (int i = 0; i < edges.Length; i++)
                            yield return edges[i];
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            TEdge[] edges;
            if (this.vertexOutEdges.TryGetValue(edge.Source, out edges) &&
                edges != null)
                for (int i = 0; i < edges.Length; i++)
                    if (edges[i].Equals(edge))
                        return true;
            return false;
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Returns self since this class is immutable
        /// </summary>
        /// <returns></returns>
        public ArrayAdjacencyGraph<TVertex, TEdge> Clone()
        {
            return this;
        }

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif
        #endregion
    }
}
