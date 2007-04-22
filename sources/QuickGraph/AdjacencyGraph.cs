using System;
using System.Collections.Generic;

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
            return this.vertexEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(Vertex v)
        {
            return this.vertexEdges[v].Count == 0;
        }

        public int OutDegree(Vertex v)
        {
            return this.vertexEdges[v].Count;
        }

        public IEnumerable<Edge> OutEdges(Vertex v)
        {
            return this.vertexEdges[v];
        }

        public Edge OutEdge(Vertex v, int index)
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
                System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
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
            if (!this.ContainsVertex(source))
                throw new VertexNotFoundException(source.ToString());
            if (!this.ContainsVertex(target))
                throw new VertexNotFoundException(target.ToString());
            foreach (Edge outEdge in this.OutEdges(source))
                if (outEdge.Target.Equals(target))
                    return true;
            return false;
        }

        public bool ContainsEdge(Edge edge)
        {
            return this.vertexEdges[edge.Source].Contains(edge);
        }

        public virtual void AddVertex(Vertex v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            if (this.EdgeCapacity>0)
                this.vertexEdges.Add(v, new EdgeList(this.EdgeCapacity));
            else
                this.vertexEdges.Add(v, new EdgeList());
            this.OnVertexAdded(new VertexEventArgs<Vertex>(v));
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
            if (!this.ContainsVertex(v))
                return false;
            // remove outedges
            this.edgeCount -= this.vertexEdges[v].Count;
            this.vertexEdges.Remove(v);

            // iterage over edges and remove each edge touching the verte
            List<Edge> edgeToRemove = new List<Edge>();
            foreach (Edge edge in this.Edges)
                if (edge.Target.Equals(v))
                    edgeToRemove.Add(edge);
            // removing edges
            foreach (Edge edge in edgeToRemove)
                this.vertexEdges[edge.Source].Remove(edge);

            // update count
            this.edgeCount -= edgeToRemove.Count;
            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);

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

        public int RemoveVertexIf(IVertexPredicate<Vertex> predicate)
        {
            VertexList vertices = new VertexList();
            foreach (Vertex v in this.Vertices)
                if (predicate.Test(v))
                    vertices.Add(v);

            foreach (Vertex v in vertices)
                this.RemoveVertex(v);

            return vertices.Count;
        }

        public virtual bool AddEdge(Edge e)
        {
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

        public int RemoveEdgeIf(IEdgePredicate<Vertex, Edge> predicate)
        {
            EdgeList edges = new EdgeList();
            foreach (Edge edge in this.Edges)
                if (predicate.Test(edge))
                    edges.Add(edge);

            foreach (Edge edge in edges)
                this.RemoveEdge(edge);

            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
            return edges.Count;
        }

        public void ClearOutEdges(Vertex v)
        {
            int count = this.OutDegree(v);
            this.vertexEdges[v].Clear();
            this.edgeCount -= count;
            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
        }

        public int RemoveOutEdgeIf(Vertex v, IEdgePredicate<Vertex, Edge> predicate)
        {
            EdgeList edges = new EdgeList();
            foreach (Edge edge in this.OutEdges(v))
                if (predicate.Test(edge))
                    edges.Add(edge);
            foreach (Edge edge in edges)
                this.RemoveEdge(edge);
            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
            return edges.Count;
        }

        public void Clear()
        {
            this.vertexEdges.Clear();
            this.edgeCount = 0;
        }

        [Serializable]
        public sealed class VertexList : List<Vertex>
        {
            public VertexList() { }
            public VertexList(int capacity)
                : base(capacity) { }
        }

        [Serializable]
        public sealed class EdgeList : List<Edge>
        {
            public EdgeList() { }
            public EdgeList(int capacity)
                : base(capacity)
            { }
        }

        [Serializable]
        public sealed class VertexEdgeDictionary : Dictionary<Vertex, EdgeList>
        {
            public VertexEdgeDictionary() { }
            public VertexEdgeDictionary(int capacity)
                : base(capacity)
            { }
        }
    }
}
