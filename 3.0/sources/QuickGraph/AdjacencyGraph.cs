using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    /// <summary>
    /// A directed graph data structure efficient for sparse
    /// graph representation where out-edge need to be enumerated only.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    [Serializable]
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class AdjacencyGraph<TVertex,TEdge> 
        : IVertexAndEdgeListGraph<TVertex,TEdge>
        , IEdgeListAndIncidenceGraph<TVertex,TEdge>
        , IMutableEdgeListGraph<TVertex,TEdge>
        , IMutableIncidenceGraph<TVertex,TEdge>
        , IMutableVertexListGraph<TVertex,TEdge>
        , IMutableVertexAndEdgeListGraph<TVertex,TEdge>
        , ICloneable
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParallelEdges;
        private readonly VertexEdgeDictionary<TVertex, TEdge> vertexEdges;
        private int edgeCount = 0;
        private int edgeCapacity = -1;

        public AdjacencyGraph()
            :this(true)
        {}

        public AdjacencyGraph(bool allowParallelEdges)
            :this(allowParallelEdges,-1)
        {
        }

        public AdjacencyGraph(bool allowParallelEdges, int capacity)
            :this(allowParallelEdges, capacity, -1)
        {
        }

        public AdjacencyGraph(bool allowParallelEdges, int capacity, int edgeCapacity)
        {
            this.allowParallelEdges = allowParallelEdges;
            if (capacity > -1)
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>(capacity);
            else
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>();
            this.edgeCapacity = edgeCapacity;
        }

        public bool IsDirected
        {
            [Pure]
            get { return this.isDirected; }
        }

        public bool AllowParallelEdges
        {
            [Pure]
            get { return this.allowParallelEdges; }
        }

        public int EdgeCapacity
        {
            [Pure]
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
        }

        public static Type VertexType
        {
            [Pure]
            get { return typeof(TVertex); }
        }

        public static Type EdgeType
        {
            [Pure]
            get { return typeof(TEdge); }
        }

        public bool IsVerticesEmpty
        {
            [Pure]
            get { return this.vertexEdges.Count == 0; }
        }

        public int VertexCount
        {
            [Pure]
            get { return this.vertexEdges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            [Pure]
            get { return this.vertexEdges.Keys; }
        }

        [Pure]
        public bool ContainsVertex(TVertex v)
        {
            Contract.Requires(v != null);

            return this.vertexEdges.ContainsKey(v);
        }

        [Pure]
        public bool IsOutEdgesEmpty(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexEdges[v].Count == 0;
        }

        [Pure]
        public int OutDegree(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexEdges[v].Count;
        }

        [Pure]
        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexEdges[v];
        }

        [Pure]
        public TEdge OutEdge(TVertex v, int index)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

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
            [Pure]
            get 
            {
                return this.edgeCount; 
            }
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.edgeCount >= 0);
        }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <value>The edges.</value>
        public IEnumerable<TEdge> Edges
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
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(GraphContract.InVertexSet(this, source));
            Contract.Requires(GraphContract.InVertexSet(this, target));

            foreach (var outEdge in this.OutEdges(source))
                if (outEdge.Target.Equals(target))
                    return true;
            return false;
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            Contract.Requires(edge != null);
            Contract.Requires(GraphContract.InVertexSet(this, edge));

            return this.vertexEdges[edge.Source].Contains(edge);
        }

        [Pure]
        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(GraphContract.InVertexSet(this, source));
            Contract.Requires(GraphContract.InVertexSet(this, target));

            EdgeList<TVertex, TEdge> edgeList;
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
        public bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            Contract.Requires(GraphContract.InVertexSet(this, source));
            Contract.Requires(GraphContract.InVertexSet(this, target));

            EdgeList<TVertex, TEdge> outEdges;
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

        public virtual void AddVertex(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(!GraphContract.InVertexSet(this, v));

            if (this.EdgeCapacity>0)
                this.vertexEdges.Add(v, new EdgeList<TVertex,TEdge>(this.EdgeCapacity));
            else
                this.vertexEdges.Add(v, new EdgeList<TVertex, TEdge>());
            this.OnVertexAdded(new VertexEventArgs<TVertex>(v));
        }

        public virtual void AddVertexRange(IEnumerable<TVertex> vertices)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(Contract.ForAll(vertices, v => v != null));

            foreach (var v in vertices)
                this.AddVertex(v);
        }

        public event VertexEventHandler<TVertex> VertexAdded;
        protected virtual void OnVertexAdded(VertexEventArgs<TVertex> args)
        {
            Contract.Requires(args != null);

            VertexEventHandler<TVertex> eh = this.VertexAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveVertex(TVertex v)
        {
            Contract.Requires(v != null);

            if (!this.ContainsVertex(v))
                return false;
            // remove outedges
            {
                var edges = this.vertexEdges[v];
                if (this.EdgeRemoved != null) // lazily notify
                {
                    foreach (var edge in edges)
                        this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
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
                    this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
                }
                // update count
                this.edgeCount -= edgeToRemove.Count;
                edgeToRemove.Clear();
            }

            Contract.Assert(this.edgeCount >= 0);
            this.vertexEdges.Remove(v);
            this.OnVertexRemoved(new VertexEventArgs<TVertex>(v));

            return true;
        }

        public event VertexEventHandler<TVertex> VertexRemoved;
        protected virtual void OnVertexRemoved(VertexEventArgs<TVertex> args)
        {
            Contract.Requires(args != null);

            VertexEventHandler<TVertex> eh = this.VertexRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveVertexIf(VertexPredicate<TVertex> predicate)
        {
            Contract.Requires(predicate != null);

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
            Contract.Requires(e != null);

            if (!this.ContainsVertex(e.Source))
                this.AddVertex(e.Source);
            if (!this.ContainsVertex(e.Target))
                this.AddVertex(e.Target);

            return this.AddEdge(e);
        }

        public virtual bool AddEdge(TEdge e)
        {
            Contract.Requires(e != null);
            Contract.Requires(GraphContract.InVertexSet(this, e));

            if (!this.AllowParallelEdges)
            {
                if (this.ContainsEdge(e.Source, e.Target))
                    return false;
            }
            this.vertexEdges[e.Source].Add(e);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<TVertex, TEdge>(e));

            return true;
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            Contract.Requires(edges != null);
            Contract.Requires(Contract.ForAll(edges, e => e != null && GraphContract.InVertexSet(this, e)));

            foreach (var edge in edges)
                this.AddEdge(edge);
        }

        public event EdgeEventHandler<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<TVertex, TEdge> args)
        {
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveEdge(TEdge e)
        {
            Contract.Requires(e != null);
            Contract.Requires(GraphContract.InVertexSet(this, e));

            if (this.vertexEdges[e.Source].Remove(e))
            {
                this.edgeCount--;
                Contract.Assert(this.edgeCount >= 0);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(e));
                return true;
            }
            else
                return false;
        }

        public event EdgeEventHandler<TVertex, TEdge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(EdgeEventArgs<TVertex, TEdge> args)
        {
            EdgeEventHandler<TVertex, TEdge> eh = this.EdgeRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
        {
            Contract.Requires(predicate != null);

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
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            var edges = this.vertexEdges[v];
            int count = edges.Count;
            if (this.EdgeRemoved != null) // call only if someone is listening
            {
                foreach (var edge in edges)
                    this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }
            edges.Clear();
            this.edgeCount -= count;
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(predicate != null);

            var edges = this.vertexEdges[v];
            var edgeToRemove = new EdgeList<TVertex,TEdge>(edges.Count);
            foreach (var edge in edges)
                if (predicate(edge))
                    edgeToRemove.Add(edge);

            foreach (var edge in edgeToRemove)
            {
                edges.Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
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
        }

        #region ICloneable Members
        private AdjacencyGraph(
            VertexEdgeDictionary<TVertex, TEdge> vertexEdges,
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
        
        [Pure]
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion
    }
}
