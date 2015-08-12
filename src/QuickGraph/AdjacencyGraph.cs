using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    /// <summary>
    /// A mutable directed graph data structure efficient for sparse
    /// graph representation where out-edge need to be enumerated only.
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class AdjacencyGraph<TVertex,TEdge> 
        : IVertexAndEdgeListGraph<TVertex,TEdge>
        , IEdgeListAndIncidenceGraph<TVertex,TEdge>
        , IMutableEdgeListGraph<TVertex,TEdge>
        , IMutableIncidenceGraph<TVertex,TEdge>
        , IMutableVertexListGraph<TVertex,TEdge>
        , IMutableVertexAndEdgeListGraph<TVertex,TEdge>
#if !SILVERLIGHT
        , ICloneable
#endif
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParallelEdges;
        private readonly IVertexEdgeDictionary<TVertex, TEdge> vertexEdges;
        private int edgeCount = 0;
        private int edgeCapacity = -1;

        public AdjacencyGraph()
            :this(true)
        {}

        public AdjacencyGraph(bool allowParallelEdges)
            :this(allowParallelEdges,-1)
        {
        }

        public AdjacencyGraph(bool allowParallelEdges, int vertexCapacity)
            :this(allowParallelEdges, vertexCapacity, -1)
        {
        }

        public AdjacencyGraph(bool allowParallelEdges, int vertexCapacity, int edgeCapacity)
            :this(allowParallelEdges, vertexCapacity, edgeCapacity, EqualityComparer<TVertex>.Default)
        {
        }

        public AdjacencyGraph(bool allowParallelEdges, int vertexCapacity, int edgeCapacity, IEqualityComparer<TVertex> vertexComparer)
        {
            Contract.Requires(vertexComparer != null);

            this.allowParallelEdges = allowParallelEdges;
            if (vertexCapacity > -1)
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>(vertexCapacity, vertexComparer);
            else
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>(vertexComparer);
            this.edgeCapacity = edgeCapacity;
        }

        public AdjacencyGraph(
            bool allowParallelEdges, 
            int capacity, 
            int edgeCapacity,
            Func<int, IVertexEdgeDictionary<TVertex, TEdge>> vertexEdgesDictionaryFactory)
        {
            Contract.Requires(vertexEdgesDictionaryFactory != null);
            this.allowParallelEdges = allowParallelEdges;
            this.vertexEdges = vertexEdgesDictionaryFactory(capacity);
            this.edgeCapacity = edgeCapacity;
        }

        public bool IsDirected
        {
            get { return this.isDirected; }
        }

        public bool AllowParallelEdges
        {
            [Pure]
            get { return this.allowParallelEdges; }
        }

        public int EdgeCapacity
        {
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
        }

        public static Type EdgeType
        {
            get { return typeof(TEdge); }
        }

        public bool IsVerticesEmpty
        {
            get { return this.vertexEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexEdges.Count; }
        }

        public virtual IEnumerable<TVertex> Vertices
        {
            get { return this.vertexEdges.Keys; }
        }

        [Pure]
        public bool ContainsVertex(TVertex v)
        {
            return this.vertexEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.vertexEdges[v].Count == 0;
        }

        public int OutDegree(TVertex v)
        {
            return this.vertexEdges[v].Count;
        }

        public virtual IEnumerable<TEdge> OutEdges(TVertex v)
        {
            return this.vertexEdges[v];
        }

        public virtual bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            IEdgeList<TVertex, TEdge> list;
            if (this.vertexEdges.TryGetValue(v, out list))
            {
                edges = list;
                return true;
            }

            edges = null;
            return false;
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            return this.vertexEdges[v][index];
        }

        /// <summary>
        /// Gets a value indicating whether this instance is edges empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is edges empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEdgesEmpty
        {
            [Pure]
            get { return this.edgeCount == 0; }
        }

        /// <summary>
        /// Gets the edge count.
        /// </summary>
        /// <value>The edge count.</value>
        public int EdgeCount
        {
            get 
            {
                return this.edgeCount; 
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(this.edgeCount >= 0);
        }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <value>The edges.</value>
        public virtual IEnumerable<TEdge> Edges
        {
            [Pure]
            get
            {
                foreach (var edges in this.vertexEdges.Values)
                    foreach (var edge in edges)
                        yield return edge;
            }
        }

        [Pure]
        public bool ContainsEdge(TVertex source, TVertex target)
        {
            IEnumerable<TEdge> outEdges;
            if (!this.TryGetOutEdges(source, out outEdges))
                return false;
            foreach (var outEdge in outEdges)
                if (outEdge.Target.Equals(target))
                    return true;
            return false;
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            IEdgeList<TVertex, TEdge> edges;
            return 
                this.vertexEdges.TryGetValue(edge.Source, out edges) &&
                edges.Contains(edge);
        }

        [Pure]
        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            IEdgeList<TVertex, TEdge> edgeList;
            if (this.vertexEdges.TryGetValue(source, out edgeList) &&
                edgeList.Count > 0)
            {
                foreach (var e in edgeList)
                {
                    if (e.Target.Equals(target))
                    {
                        edge = e;
                        return true;
                    }
                }
            }
            edge = default(TEdge);
            return false;
        }

        [Pure]
        public virtual bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            IEdgeList<TVertex, TEdge> outEdges;
            if (this.vertexEdges.TryGetValue(source, out outEdges))
            {
                List<TEdge> list = new List<TEdge>(outEdges.Count);
                foreach (var edge in outEdges)
                    if (edge.Target.Equals(target))
                        list.Add(edge);

                edges = list;
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        public virtual bool AddVertex(TVertex v)
        {
            if (this.ContainsVertex(v))
                return false;

            if (this.EdgeCapacity>0)
                this.vertexEdges.Add(v, new EdgeList<TVertex,TEdge>(this.EdgeCapacity));
            else
                this.vertexEdges.Add(v, new EdgeList<TVertex, TEdge>());
            this.OnVertexAdded(v);
            return true;
        }

        public virtual int AddVertexRange(IEnumerable<TVertex> vertices)
        {
            int count = 0;
            foreach (var v in vertices)
                if (this.AddVertex(v))
                    count++;
            return count;
        }

        public event VertexAction<TVertex> VertexAdded;
        protected virtual void OnVertexAdded(TVertex args)
        {
            Contract.Requires(args != null);

            var eh = this.VertexAdded;
            if (eh != null)
                eh(args);
        }

        public virtual bool RemoveVertex(TVertex v)
        {
            if (!this.ContainsVertex(v))
                return false;
            // remove outedges
            {
                var edges = this.vertexEdges[v];
                if (this.EdgeRemoved != null) // lazily notify
                {
                    foreach (var edge in edges)
                        this.OnEdgeRemoved(edge);
                }
                this.edgeCount -= edges.Count;
                edges.Clear();
            }

            // iterage over edges and remove each edge touching the vertex
            var edgeToRemove = new EdgeList<TVertex, TEdge>();
            foreach (var kv in this.vertexEdges)
            {
                if (kv.Key.Equals(v)) continue; // we've already 
                // collect edge to remove
                foreach(var edge in kv.Value)
                {
                    if (edge.Target.Equals(v))
                        edgeToRemove.Add(edge);
                }

                // remove edges
                foreach (var edge in edgeToRemove)
                {
                    kv.Value.Remove(edge);
                    this.OnEdgeRemoved(edge);
                }
                // update count
                this.edgeCount -= edgeToRemove.Count;
                edgeToRemove.Clear();
            }

            Contract.Assert(this.edgeCount >= 0);
            this.vertexEdges.Remove(v);
            this.OnVertexRemoved(v);

            return true;
        }

        public event VertexAction<TVertex> VertexRemoved;
        protected virtual void OnVertexRemoved(TVertex args)
        {
            Contract.Requires(args != null);

            var eh = this.VertexRemoved;
            if (eh != null)
                eh(args);
        }

        public int RemoveVertexIf(VertexPredicate<TVertex> predicate)
        {
            var vertices = new VertexList<TVertex>();
            foreach (var v in this.Vertices)
                if (predicate(v))
                    vertices.Add(v);

            foreach (var v in vertices)
                this.RemoveVertex(v);

            return vertices.Count;
        }

        public virtual bool AddVerticesAndEdge(TEdge e)
        {
            this.AddVertex(e.Source);
            this.AddVertex(e.Target);
            return this.AddEdge(e);
        }

        /// <summary>
        /// Adds a range of edges to the graph
        /// </summary>
        /// <param name="edges"></param>
        /// <returns>the count edges that were added</returns>
        public int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddVerticesAndEdge(edge))
                    count++;
            return count;
        }

        /// <summary>
        /// Adds the edge to the graph
        /// </summary>
        /// <param name="e">the edge to add</param>
        /// <returns>true if the edge was added; false if it was already part of the graph</returns>
        public virtual bool AddEdge(TEdge e)
        {
            if (!this.AllowParallelEdges)
            {
                if (this.ContainsEdge(e.Source, e.Target))
                    return false;
            }
            this.vertexEdges[e.Source].Add(e);
            this.edgeCount++;

            this.OnEdgeAdded(e);

            return true;
        }

        public int AddEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddEdge(edge))
                    count++;
            return count;
        }

        public event EdgeAction<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(TEdge args)
        {
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(args);
        }

        public virtual bool RemoveEdge(TEdge e)
        {
            IEdgeList<TVertex, TEdge> edges;
            if (this.vertexEdges.TryGetValue(e.Source, out edges) &&
                edges.Remove(e))
            {
                this.edgeCount--;
                Contract.Assert(this.edgeCount >= 0);
                this.OnEdgeRemoved(e);
                return true;
            }
            else
                return false;
        }

        public event EdgeAction<TVertex, TEdge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(TEdge args)
        {
            var eh = this.EdgeRemoved;
            if (eh != null)
                eh(args);
        }

        public int RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
        {
            var edges = new EdgeList<TVertex, TEdge>();
            foreach (var edge in this.Edges)
                if (predicate(edge))
                    edges.Add(edge);

            foreach (var edge in edges)
                this.RemoveEdge(edge);

            return edges.Count;
        }

        public void ClearOutEdges(TVertex v)
        {
            var edges = this.vertexEdges[v];
            int count = edges.Count;
            if (this.EdgeRemoved != null) // call only if someone is listening
            {
                foreach (var edge in edges)
                    this.OnEdgeRemoved(edge);
            }
            edges.Clear();
            this.edgeCount -= count;
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            var edges = this.vertexEdges[v];
            var edgeToRemove = new EdgeList<TVertex,TEdge>(edges.Count);
            foreach (var edge in edges)
                if (predicate(edge))
                    edgeToRemove.Add(edge);

            foreach (var edge in edgeToRemove)
            {
                edges.Remove(edge);
                this.OnEdgeRemoved(edge);
            }
            this.edgeCount -= edgeToRemove.Count;

            return edgeToRemove.Count;
        }

        public void TrimEdgeExcess()
        {
            foreach (var edges in this.vertexEdges.Values)
                edges.TrimExcess();
        }

        public void Clear()
        {
            this.vertexEdges.Clear();
            this.edgeCount = 0;
            this.OnCleared(EventArgs.Empty);
        }

        public event EventHandler Cleared;
        private void OnCleared(EventArgs e)
        {
            var eh = this.Cleared;
            if (eh != null)
                eh(this, e);
        }

        #region ICloneable Members
        private AdjacencyGraph(
            IVertexEdgeDictionary<TVertex, TEdge> vertexEdges,
            int edgeCount,
            int edgeCapacity,
            bool allowParallelEdges
            )
        {
            Contract.Requires(vertexEdges != null);
            Contract.Requires(edgeCount >= 0);

            this.vertexEdges = vertexEdges;
            this.edgeCount = edgeCount;
            this.edgeCapacity = edgeCapacity;
            this.allowParallelEdges = allowParallelEdges;
        }

        [Pure]
        public AdjacencyGraph<TVertex, TEdge> Clone()
        {
            return new AdjacencyGraph<TVertex, TEdge>(
                this.vertexEdges.Clone(),
                this.edgeCount,
                this.edgeCapacity,
                this.allowParallelEdges
                );
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
