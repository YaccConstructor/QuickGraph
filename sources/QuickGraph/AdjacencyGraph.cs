using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickGraph
{
    [Serializable]
    public class AdjacencyGraph<Vertex,Edge> : 
        IVertexAndEdgeListGraph<Vertex,Edge>,
        IEdgeListAndIncidenceGraph<Vertex,Edge>,
        IMutableEdgeListGraph<Vertex,Edge>,
        IMutableIncidenceGraph<Vertex,Edge>,
        IMutableVertexListGraph<Vertex,Edge>,
        IMutableVertexAndEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
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
            if (capacity > 0)
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
            get { return typeof(Vertex); }
        }

        public static Type EdgeType
        {
            get { return typeof(Edge); }
        }

        public bool IsVerticesEmpty
        {
            get { return this.vertexEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexEdges.Count; }
        }

        public IEnumerable<Vertex> Vertices
        {
            get { return this.vertexEdges.Keys; }
        }

        public bool ContainsVertex(Vertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            return this.vertexEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v].Count == 0;
        }

        public int OutDegree(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v].Count;
        }

        public IEnumerable<Edge> OutEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexEdges[v];
        }

        public Edge OutEdge(Vertex v, int index)
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
        public IEnumerable<Edge> Edges
        {
            get
            {
                foreach (EdgeList edges in this.vertexEdges.Values)
                    foreach (Edge edge in edges)
                        yield return edge;
            }
        }

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");
            foreach (Edge outEdge in this.OutEdges(source))
                if (outEdge.Target.Equals(target))
                    return true;
            return false;
        }

        public bool ContainsEdge(Edge edge)
        {
            GraphContracts.AssumeInVertexSet(this, edge, "edge");
            return this.vertexEdges[edge.Source].Contains(edge);
        }

        public bool TryGetEdge(
            Vertex source,
            Vertex target,
            out Edge edge)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList edgeList;
            if (this.vertexEdges.TryGetValue(source, out edgeList) &&
                edgeList.Count > 0)
            {
                foreach (Edge e in edgeList)
                {
                    if (e.Target.Equals(target))
                    {
                        edge = e;
                        return true;
                    }
                }
            }
            edge = default(Edge);
            return false;
        }

        public bool TryGetEdges(
            Vertex source,
            Vertex target,
            out IEnumerable<Edge> edges)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList outEdges;
            if (this.vertexEdges.TryGetValue(source, out outEdges))
            {
                List<Edge> list = new List<Edge>(outEdges.Count);
                foreach (Edge edge in outEdges)
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

        public virtual void AddVertex(Vertex v)
        {
            GraphContracts.AssumeNotInVertexSet(this, v, "v");
            if (this.EdgeCapacity>0)
                this.vertexEdges.Add(v, new EdgeList(this.EdgeCapacity));
            else
                this.vertexEdges.Add(v, new EdgeList());
            this.OnVertexAdded(new VertexEventArgs<Vertex>(v));
        }

        public virtual void AddVertexRange(IEnumerable<Vertex> vertices)
        {
            GraphContracts.AssumeNotNull(vertices, "vertices");
            foreach (Vertex v in vertices)
                this.AddVertex(v);
        }

        public event VertexEventHandler<Vertex> VertexAdded;
        protected virtual void OnVertexAdded(VertexEventArgs<Vertex> args)
        {
            VertexEventHandler<Vertex> eh = this.VertexAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveVertex(Vertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            if (!this.ContainsVertex(v))
                return false;
            // remove outedges
            {
                EdgeList edges = this.vertexEdges[v];
                if (this.EdgeRemoved != null) // lazily notify
                {
                    foreach (Edge edge in edges)
                        this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
                }
                this.edgeCount -= edges.Count;
                edges.Clear();
            }

            // iterage over edges and remove each edge touching the vertex
            EdgeList edgeToRemove = new EdgeList();
            foreach (KeyValuePair<Vertex,EdgeList> kv in this.vertexEdges)
            {
                if (kv.Key.Equals(v)) continue; // we've already 
                // collect edge to remove
                foreach(Edge edge in kv.Value)
                {
                    if (edge.Target.Equals(v))
                        edgeToRemove.Add(edge);
                }

                // remove edges
                foreach (Edge edge in edgeToRemove)
                {
                    kv.Value.Remove(edge);
                    this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
                }
                // update count
                this.edgeCount -= edgeToRemove.Count;
                edgeToRemove.Clear();
            }

            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
            this.vertexEdges.Remove(v);
            this.OnVertexRemoved(new VertexEventArgs<Vertex>(v));

            return true;
        }

        public event VertexEventHandler<Vertex> VertexRemoved;
        protected virtual void OnVertexRemoved(VertexEventArgs<Vertex> args)
        {
            VertexEventHandler<Vertex> eh = this.VertexRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveVertexIf(VertexPredicate<Vertex> predicate)
        {
            GraphContracts.AssumeNotNull(predicate, "predicate");
            VertexList vertices = new VertexList();
            foreach (Vertex v in this.Vertices)
                if (predicate(v))
                    vertices.Add(v);

            foreach (Vertex v in vertices)
                this.RemoveVertex(v);

            return vertices.Count;
        }

        public virtual bool AddEdge(Edge e)
        {
            GraphContracts.AssumeInVertexSet<Vertex, Edge>(this, e, "e");
            if (!this.AllowParallelEdges)
            {
                if (this.ContainsEdge(e.Source, e.Target))
                    return false;
            }
            this.vertexEdges[e.Source].Add(e);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<Vertex, Edge>(e));

            return true;
        }

        public event EdgeEventHandler<Vertex, Edge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<Vertex, Edge> args)
        {
            EdgeEventHandler<Vertex, Edge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveEdge(Edge e)
        {
            GraphContracts.AssumeInVertexSet(this, e, "e");
            if (this.vertexEdges[e.Source].Remove(e))
            {
                this.edgeCount--;
                System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(e));
                return true;
            }
            else
                return false;
        }

        public event EdgeEventHandler<Vertex, Edge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(EdgeEventArgs<Vertex, Edge> args)
        {
            EdgeEventHandler<Vertex, Edge> eh = this.EdgeRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveEdgeIf(EdgePredicate<Vertex, Edge> predicate)
        {
            GraphContracts.AssumeNotNull(predicate, "predicate");

            EdgeList edges = new EdgeList();
            foreach (Edge edge in this.Edges)
                if (predicate(edge))
                    edges.Add(edge);

            foreach (Edge edge in edges)
                this.RemoveEdge(edge);

            GraphContracts.Assert(this.edgeCount >= 0);
            return edges.Count;
        }

        public void ClearOutEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            EdgeList edges = this.vertexEdges[v];
            int count = edges.Count;
            if (this.EdgeRemoved != null) // call only if someone is listening
            {
                foreach (Edge edge in edges)
                    this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
            }
            edges.Clear();
            this.edgeCount -= count;
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public int RemoveOutEdgeIf(Vertex v, EdgePredicate<Vertex, Edge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");

            EdgeList edges = this.vertexEdges[v];
            EdgeList edgeToRemove = new EdgeList(edges.Count);
            foreach (Edge edge in edges)
            {
                if (predicate(edge))
                    edgeToRemove.Add(edge);
            }
            foreach (Edge edge in edgeToRemove)
            {
                edges.Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
            }
            this.edgeCount -= edgeToRemove.Count;
            GraphContracts.Assert(this.edgeCount >= 0);

            return edgeToRemove.Count;
        }

        public void Clear()
        {
            this.vertexEdges.Clear();
            this.edgeCount = 0;
        }

        [Serializable]
        public sealed class VertexList : List<Vertex>
        {
            internal VertexList() { }
            internal VertexList(int capacity)
                : base(capacity) { }
        }

        [Serializable]
        public sealed class EdgeList : List<Edge>
        {
            internal EdgeList() { }
            internal EdgeList(int capacity)
                : base(capacity)
            { }
        }

        [Serializable]
        public sealed class VertexEdgeDictionary : Dictionary<Vertex, EdgeList>
        {
            internal VertexEdgeDictionary() { }
            internal VertexEdgeDictionary(int capacity)
                : base(capacity)
            { }
        }
    }
}
