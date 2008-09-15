using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickGraph
{
    [Serializable]
    public class AdjacencyGraph<TVertex,TEdge> : 
        IVertexAndEdgeListGraph<TVertex,TEdge>,
        IEdgeListAndIncidenceGraph<TVertex,TEdge>,
        IMutableEdgeListGraph<TVertex,TEdge>,
        IMutableIncidenceGraph<TVertex,TEdge>,
        IMutableVertexListGraph<TVertex,TEdge>,
        IMutableVertexAndEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParallelEdges;
        private readonly VertexEdgeDictionary vertexEdges;
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
        {
            this.allowParallelEdges = allowParallelEdges;
            if (capacity > -1)
                this.vertexEdges = new VertexEdgeDictionary(capacity);
            else
                this.vertexEdges = new VertexEdgeDictionary();
        }

        public bool IsDirected
        {
            get { return this.isDirected; }
        }

        public bool AllowParallelEdges
        {
            get { return this.allowParallelEdges; }
        }

        public int EdgeCapacity
        {
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
        }

        public static Type VertexType
        {
            get { return typeof(TVertex); }
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

        public IEnumerable<TVertex> Vertices
        {
            get { return this.vertexEdges.Keys; }
        }

        public bool ContainsVertex(TVertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            return this.vertexEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v].Count == 0;
        }

        public int OutDegree(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v].Count;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v];
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
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
                GraphContracts.Assert(this.edgeCount >= 0);
                return this.edgeCount; 
            }
        }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <value>The edges.</value>
        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (EdgeList edges in this.vertexEdges.Values)
                    foreach (var edge in edges)
                        yield return edge;
            }
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");
            foreach (var outEdge in this.OutEdges(source))
                if (outEdge.Target.Equals(target))
                    return true;
            return false;
        }

        public bool ContainsEdge(TEdge edge)
        {
            GraphContracts.AssumeInVertexSet(this, edge, "edge");
            return this.vertexEdges[edge.Source].Contains(edge);
        }

        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList edgeList;
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

        public bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList outEdges;
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
            GraphContracts.AssumeNotInVertexSet(this, v, "v");
            if (this.EdgeCapacity>0)
                this.vertexEdges.Add(v, new EdgeList(this.EdgeCapacity));
            else
                this.vertexEdges.Add(v, new EdgeList());
            this.OnVertexAdded(new VertexEventArgs<TVertex>(v));
        }

        public virtual void AddVertexRange(IEnumerable<TVertex> vertices)
        {
            GraphContracts.AssumeNotNull(vertices, "vertices");
            foreach (var v in vertices)
                this.AddVertex(v);
        }

        public event VertexEventHandler<TVertex> VertexAdded;
        protected virtual void OnVertexAdded(VertexEventArgs<TVertex> args)
        {
            VertexEventHandler<TVertex> eh = this.VertexAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveVertex(TVertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            if (!this.ContainsVertex(v))
                return false;
            // remove outedges
            {
                EdgeList edges = this.vertexEdges[v];
                if (this.EdgeRemoved != null) // lazily notify
                {
                    foreach (var edge in edges)
                        this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
                }
                this.edgeCount -= edges.Count;
                edges.Clear();
            }

            // iterage over edges and remove each edge touching the vertex
            EdgeList edgeToRemove = new EdgeList();
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

            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
            this.vertexEdges.Remove(v);
            this.OnVertexRemoved(new VertexEventArgs<TVertex>(v));

            return true;
        }

        public event VertexEventHandler<TVertex> VertexRemoved;
        protected virtual void OnVertexRemoved(VertexEventArgs<TVertex> args)
        {
            VertexEventHandler<TVertex> eh = this.VertexRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveVertexIf(VertexPredicate<TVertex> predicate)
        {
            GraphContracts.AssumeNotNull(predicate, "predicate");
            VertexList vertices = new VertexList();
            foreach (var v in this.Vertices)
                if (predicate(v))
                    vertices.Add(v);

            foreach (var v in vertices)
                this.RemoveVertex(v);

            return vertices.Count;
        }

        public virtual bool AddVerticesAndEdge(TEdge e)
        {
            GraphContracts.AssumeNotNull(e, "e");
            if (!this.ContainsVertex(e.Source))
                this.AddVertex(e.Source);
            if (!this.ContainsVertex(e.Target))
                this.AddVertex(e.Target);

            return this.AddEdge(e);
        }

        public virtual bool AddEdge(TEdge e)
        {
            GraphContracts.AssumeInVertexSet<TVertex, TEdge>(this, e, "e");
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
            GraphContracts.AssumeNotNull(edges, "edges");
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
            GraphContracts.AssumeInVertexSet(this, e, "e");
            if (this.vertexEdges[e.Source].Remove(e))
            {
                this.edgeCount--;
                System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
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
            GraphContracts.AssumeNotNull(predicate, "predicate");

            var edges = new EdgeList();
            foreach (var edge in this.Edges)
                if (predicate(edge))
                    edges.Add(edge);

            foreach (var edge in edges)
                this.RemoveEdge(edge);

            GraphContracts.Assert(this.edgeCount >= 0);
            return edges.Count;
        }

        public void ClearOutEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            var edges = this.vertexEdges[v];
            int count = edges.Count;
            if (this.EdgeRemoved != null) // call only if someone is listening
            {
                foreach (var edge in edges)
                    this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }
            edges.Clear();
            this.edgeCount -= count;
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");

            var edges = this.vertexEdges[v];
            var edgeToRemove = new EdgeList(edges.Count);
            foreach (var edge in edges)
                if (predicate(edge))
                    edgeToRemove.Add(edge);

            foreach (var edge in edgeToRemove)
            {
                edges.Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }
            this.edgeCount -= edgeToRemove.Count;
            GraphContracts.Assert(this.edgeCount >= 0);

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

        [Serializable]
        public sealed class VertexList : List<TVertex>
        {
            internal VertexList() { }
            internal VertexList(int capacity)
                : base(capacity) { }
        }

        [Serializable]
        public sealed class EdgeList : List<TEdge>
        {
            internal EdgeList() { }
            internal EdgeList(int capacity)
                : base(capacity)
            { }
        }

        [Serializable]
        public sealed class VertexEdgeDictionary : Dictionary<TVertex, EdgeList>
        {
            internal VertexEdgeDictionary() { }
            internal VertexEdgeDictionary(int capacity)
                : base(capacity)
            { }
        }
    }
}
