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
    public sealed class ArrayBidirectionalGraph<TVertex, TEdge>
        : IBidirectionalGraph<TVertex, TEdge>
#if !SILVERLIGHT
        , ICloneable
#endif
        where TEdge : IEdge<TVertex>
    {
        readonly Dictionary<TVertex, InOutEdges> vertexEdges;
        readonly int edgeCount;

#if !SILVERLIGHT
        [Serializable]
#endif
        struct InOutEdges
        {
            private readonly TEdge[] _outEdges;
            private readonly TEdge[] _inEdges;
            public InOutEdges(TEdge[] outEdges, TEdge[] inEdges)
            {
                this._outEdges = outEdges != null && outEdges.Length > 0 ? outEdges : null;
                this._inEdges = inEdges != null && inEdges.Length > 0 ? inEdges : null;
            }
            public bool TryGetOutEdges(out TEdge[] edges)
            {
                edges=  this._outEdges;
                return edges != null;
            }
            public bool TryGetInEdges(out TEdge[] edges)
            {
                edges=  this._inEdges;
                return edges != null;
            }
        }

        /// <summary>
        /// Constructs a new ArrayBidirectionalGraph instance from a 
        /// IBidirectionalGraph instance
        /// </summary>
        /// <param name="visitedGraph"></param>
        public ArrayBidirectionalGraph(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph
            )
        {
            Contract.Requires(visitedGraph != null);

            this.vertexEdges = new Dictionary<TVertex, InOutEdges>(visitedGraph.VertexCount);
            this.edgeCount = visitedGraph.EdgeCount;
            foreach (var vertex in visitedGraph.Vertices)
            {
                var outEdges = Enumerable.ToArray(visitedGraph.OutEdges(vertex));
                var inEdges = Enumerable.ToArray(visitedGraph.InEdges(vertex));
                this.vertexEdges.Add(vertex, new InOutEdges(outEdges, inEdges));
            }
        }

        private ArrayBidirectionalGraph(
            Dictionary<TVertex, InOutEdges> vertexEdges,
            int edgeCount
            )
        {
            Contract.Requires(vertexEdges != null);
            Contract.Requires(edgeCount >= 0);

            this.vertexEdges = vertexEdges;
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
            InOutEdges es;
            if (this.vertexEdges.TryGetValue(source, out es))
            {
                List<TEdge> _edges = null;
                TEdge[] outEdges;
                if (es.TryGetOutEdges(out outEdges))
                    for (int i = 0; i < outEdges.Length; i++)
                    {
                        if (outEdges[i].Target.Equals(target))
                        {
                            if (_edges == null)
                                _edges = new List<TEdge>(outEdges.Length - i);
                            _edges.Add(outEdges[i]);
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
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(source, out io) &&
                (io.TryGetOutEdges(out edges)))
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
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetOutEdges(out edges)))
                return false;
            return true;
        }

        public int OutDegree(TVertex v)
        {
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetOutEdges(out edges)))
                return edges.Length;
            return 0;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            IEnumerable<TEdge> result;
            if (this.TryGetInEdges(v, out result))
                return result;
            else
                return Enumerable.Empty<TEdge>();
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            InOutEdges io;
            TEdge[] aedges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetOutEdges(out aedges)))
            {
                edges = aedges;
                return true;
            }

            edges = null;
            return false;
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            var io = this.vertexEdges[v];

            TEdge[] edges;
            if (!io.TryGetOutEdges(out edges))
                Contract.Assert(false);

            return edges[index];
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
            return this.vertexEdges.ContainsKey(vertex);
        }
        #endregion

        #region IVertexSet<TVertex> Members
        public bool IsVerticesEmpty
        {
            get { return this.vertexEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexEdges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                return this.vertexEdges.Keys;
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
                foreach (var io in this.vertexEdges.Values)
                {
                    TEdge[] edges;
                    if (io.TryGetOutEdges(out edges))
                        for (int i = 0; i < edges.Length; i++)
                            yield return edges[i];
                }
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(edge.Source, out io) &&
                (io.TryGetOutEdges(out edges)))
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
        public ArrayBidirectionalGraph<TVertex, TEdge> Clone()
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

        #region IBidirectionalGraph<TVertex,TEdge> Members
        public bool IsInEdgesEmpty(TVertex v)
        {
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetInEdges(out edges)))
                return false;
            return true;
        }

        public int InDegree(TVertex v)
        {
            InOutEdges io;
            TEdge[] edges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetInEdges(out edges)))
                return edges.Length;
            return 0;
        }

        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            IEnumerable<TEdge> result;
            if (this.TryGetInEdges(v, out result))
                return result;
            else
                return Enumerable.Empty<TEdge>();
        }

        public bool TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            InOutEdges io;
            TEdge[] aedges;
            if (this.vertexEdges.TryGetValue(v, out io) &&
                (io.TryGetInEdges(out aedges)))
            {
                edges = aedges;
                return true;
            }

            edges = null;
            return false;
        }

        public TEdge InEdge(TVertex v, int index)
        {
            var io = this.vertexEdges[v];

            TEdge[] edges;
            if (!io.TryGetOutEdges(out edges))
                Contract.Assert(false);

            return edges[index];
        }

        public int Degree(TVertex v)
        {
            return InDegree(v) + OutDegree(v);
        }
        #endregion
    }
}
