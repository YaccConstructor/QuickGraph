using System;
using System.Collections.Generic;

namespace QuickGraph
{
    [Serializable]
    public class BidirectionalGraph<Vertex, Edge> :
        IVertexAndEdgeListGraph<Vertex, Edge>,
        IEdgeListAndIncidenceGraph<Vertex, Edge>,
        IMutableEdgeListGraph<Vertex, Edge>,
        IMutableIncidenceGraph<Vertex, Edge>,
        IMutableVertexListGraph<Vertex, Edge>,
        IBidirectionalGraph<Vertex,Edge>,
        IMutableBidirectionalGraph<Vertex,Edge>,
        IMutableVertexAndEdgeListGraph<Vertex, Edge>,
        ICloneable
        where Edge : IEdge<Vertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParallelEdges;
        private readonly VertexEdgeDictionary vertexOutEdges = new VertexEdgeDictionary();
        private readonly VertexEdgeDictionary vertexInEdges = new VertexEdgeDictionary();
        private int edgeCount = 0;
        private int edgeCapacity = -1;

        public BidirectionalGraph()
            :this(true)
        {}

        public BidirectionalGraph(bool allowParallelEdges)
            :this(allowParallelEdges,-1)
        {}

        public BidirectionalGraph(bool allowParallelEdges, int vertexCapacity)
        {
            this.allowParallelEdges = allowParallelEdges;
            if (vertexCapacity > 0)
            {
                this.vertexInEdges = new VertexEdgeDictionary(vertexCapacity);
                this.vertexOutEdges = new VertexEdgeDictionary(vertexCapacity);
            }
            else
            {
                this.vertexInEdges = new VertexEdgeDictionary();
                this.vertexOutEdges = new VertexEdgeDictionary();
            }
        }

        public static Type VertexType
        {
            get { return typeof(Vertex); }
        }

        public static Type EdgeType
        {
            get { return typeof(Edge); }
        }

        public int EdgeCapacity
        {
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
        }

        public bool IsDirected
        {
            get { return this.isDirected; }
        }

        public bool AllowParallelEdges
        {
            get { return this.allowParallelEdges; }
        }

        public bool IsVerticesEmpty
        {
            get { return this.vertexOutEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexOutEdges.Count; }
        }

        public IEnumerable<Vertex> Vertices
        {
            get { return this.vertexOutEdges.Keys; }
        }

        public bool ContainsVertex(Vertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            return this.vertexOutEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v].Count == 0;
        }

        public int OutDegree(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v].Count;
        }

        public IEnumerable<Edge> OutEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v];
        }

        public Edge OutEdge(Vertex v, int index)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v][index];
        }

        public bool IsInEdgesEmpty(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v].Count == 0;
        }

        public int InDegree(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v].Count;
        }

        public IEnumerable<Edge> InEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v];
        }

        public Edge InEdge(Vertex v, int index)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v][index];
        }

        public int Degree(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.OutDegree(v) + this.InDegree(v);
        }

        public bool IsEdgesEmpty
        {
            get { return this.edgeCount == 0; }
        }

        public int EdgeCount
        {
            get 
            {
                GraphContracts.Assert(this.edgeCount >= 0);
                return this.edgeCount; 
            }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                foreach (EdgeList edges in this.vertexOutEdges.Values)
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

        public bool TryGetEdge(
            Vertex source,
            Vertex target,
            out Edge edge)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList edgeList;
            if (this.vertexOutEdges.TryGetValue(source, out edgeList) &&
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

            EdgeList edgeList;
            if (this.vertexOutEdges.TryGetValue(source, out edgeList))
            {
                List<Edge> list = new List<Edge>(edgeList.Count);
                foreach (Edge edge in edgeList)
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

        public bool ContainsEdge(Edge edge)
        {
            GraphContracts.AssumeInVertexSet(this, edge, "edge");
            return this.vertexOutEdges[edge.Source].Contains(edge);
        }

        public virtual void AddVertex(Vertex v)
        {
            GraphContracts.AssumeNotInVertexSet(this, v, "v");

            if (this.EdgeCapacity > 0)
            {
                this.vertexOutEdges.Add(v, new EdgeList(this.EdgeCapacity));
                this.vertexInEdges.Add(v, new EdgeList(this.EdgeCapacity));
            }
            else
            {
                this.vertexOutEdges.Add(v, new EdgeList());
                this.vertexInEdges.Add(v, new EdgeList());
            }
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

            // collect edges to remove
            EdgeList edgesToRemove = new EdgeList();
            foreach (Edge outEdge in this.OutEdges(v))
            {
                this.vertexInEdges[outEdge.Target].Remove(outEdge);
                edgesToRemove.Add(outEdge);
            }
            foreach (Edge inEdge in this.InEdges(v))
            {
                // might already have been removed
                if(this.vertexOutEdges[inEdge.Source].Remove(inEdge))
                    edgesToRemove.Add(inEdge);
            }

            // notify users
            if (this.EdgeRemoved != null)
            {
                foreach(Edge edge in edgesToRemove)
                    this.OnEdgeRemoved(new EdgeEventArgs<Vertex,Edge>(edge));
            }

            this.vertexOutEdges.Remove(v);
            this.vertexInEdges.Remove(v);
            this.edgeCount -= edgesToRemove.Count;
            this.OnVertexRemoved(new VertexEventArgs<Vertex>(v));

            GraphContracts.Assert(this.edgeCount >= 0);
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
            GraphContracts.AssumeInVertexSet(this, e, "e");
            if (!this.AllowParallelEdges)
            {
                if (this.ContainsEdge(e.Source, e.Target))
                    return false;
            }
            this.vertexOutEdges[e.Source].Add(e);
            this.vertexInEdges[e.Target].Add(e);
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
            if (this.vertexOutEdges[e.Source].Remove(e))
            {
                this.vertexInEdges[e.Target].Remove(e);
                this.edgeCount--;
                GraphContracts.Assert(this.edgeCount >= 0);

                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(e));
                return true;
            }
            else
            {
                return false;
            }
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
            return edges.Count;
        }

        public int RemoveOutEdgeIf(Vertex v, EdgePredicate<Vertex, Edge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");
            EdgeList edges = new EdgeList();
            foreach (Edge edge in this.OutEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (Edge edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public int RemoveInEdgeIf(Vertex v, EdgePredicate<Vertex, Edge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");
            EdgeList edges = new EdgeList();
            foreach (Edge edge in this.InEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (Edge edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public void ClearOutEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            EdgeList outEdges = this.vertexOutEdges[v];
            foreach (Edge edge in outEdges)
            {
                this.vertexInEdges[edge.Target].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
            }

            this.edgeCount -= outEdges.Count;
            outEdges.Clear();
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public void ClearInEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            EdgeList inEdges = this.vertexInEdges[v];
            foreach (Edge edge in inEdges)
            {
                this.vertexOutEdges[edge.Source].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
            }

            this.edgeCount -= inEdges.Count;
            inEdges.Clear();
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public void ClearEdges(Vertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            ClearOutEdges(v);
            ClearInEdges(v);
        }

        public void Clear()
        {
            this.vertexOutEdges.Clear();
            this.vertexInEdges.Clear();
            this.edgeCount = 0;
        }

        public void MergeVertex(Vertex v, IEdgeFactory<Vertex, Edge> edgeFactory)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(edgeFactory, "edgeFactory");

            // storing edges in local array
            EdgeList inedges = this.vertexInEdges[v];
            EdgeList outedges = this.vertexOutEdges[v];

            // remove vertex
            this.RemoveVertex(v);

            // add edges from each source to each target
            foreach (Edge source in inedges)
            {
                //is it a self edge
                if (source.Source.Equals(v))
                    continue;
                foreach (Edge target in outedges)
                {
                    if (v.Equals(target.Target))
                        continue;
                    // we add an new edge
                    this.AddEdge(edgeFactory.CreateEdge(source.Source, target.Target));
                }
            }
        }

        public void MergeVertexIf(VertexPredicate<Vertex> vertexPredicate, IEdgeFactory<Vertex, Edge> edgeFactory)
        {
            GraphContracts.AssumeNotNull(vertexPredicate, "vertexPredicate");
            GraphContracts.AssumeNotNull(edgeFactory, "edgeFactory");

            // storing vertices to merge
            VertexList mergeVertices = new VertexList(this.VertexCount / 4);
            foreach (Vertex v in this.Vertices)
                if (vertexPredicate(v))
                    mergeVertices.Add(v);

            // applying merge recursively
            foreach (Vertex v in mergeVertices)
                MergeVertex(v, edgeFactory);
        }

        [Serializable]
        private sealed class VertexList : List<Vertex>
        {
            public VertexList() { }
            public VertexList(int capacity)
                : base(capacity) { }
        }

        [Serializable]
        private sealed class EdgeList : List<Edge>, ICloneable
        {
            public EdgeList() { }
            public EdgeList(int capacity)
                : base(capacity)
            { }
            private EdgeList(EdgeList list)
                : base(list)
            { }

            public EdgeList Clone()
            {
                return new EdgeList(this);
            }

            object ICloneable.Clone()
            {
                return this.Clone();
            }
        }

        [Serializable]
        private sealed class VertexEdgeDictionary 
            : Dictionary<Vertex, EdgeList>, ICloneable
        {
            public VertexEdgeDictionary() { }
            public VertexEdgeDictionary(int capacity)
                : base(capacity)
            { }

            public VertexEdgeDictionary Clone()
            {
                VertexEdgeDictionary clone = new VertexEdgeDictionary(this.Count);
                foreach (KeyValuePair<Vertex, EdgeList> kv in this)
                    clone.Add(kv.Key, kv.Value.Clone());
                return clone;
            }

            object ICloneable.Clone()
            {
                return this.Clone();
            }
        }

        #region ICloneable Members
        private BidirectionalGraph(
            VertexEdgeDictionary vertexInEdges,
            VertexEdgeDictionary vertexOutEdges,
            int edgeCount,
            int edgeCapacity,
            bool allowParallelEdges
            )
        {
            this.vertexInEdges = vertexInEdges;
            this.vertexOutEdges = vertexOutEdges;
            this.edgeCount = edgeCount;
            this.edgeCapacity = edgeCapacity;
            this.allowParallelEdges = allowParallelEdges;
        }

        public BidirectionalGraph<Vertex, Edge> Clone()
        {
            return new BidirectionalGraph<Vertex, Edge>(
                this.vertexInEdges.Clone(),
                this.vertexOutEdges.Clone(),
                this.edgeCount,
                this.edgeCapacity,
                this.allowParallelEdges
                );
        }
        
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion
    }
}
