using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QuickGraph
{
    [Serializable]
    public class BidirectionalGraph<TVertex, TEdge> :
        IVertexAndEdgeListGraph<TVertex, TEdge>,
        IEdgeListAndIncidenceGraph<TVertex, TEdge>,
        IMutableEdgeListGraph<TVertex, TEdge>,
        IMutableIncidenceGraph<TVertex, TEdge>,
        IMutableVertexListGraph<TVertex, TEdge>,
        IBidirectionalGraph<TVertex,TEdge>,
        IMutableBidirectionalGraph<TVertex,TEdge>,
        IMutableVertexAndEdgeListGraph<TVertex, TEdge>,
        ICloneable
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParallelEdges;
        private readonly VertexEdgeDictionary vertexOutEdges 
            = new VertexEdgeDictionary();
        private readonly VertexEdgeDictionary vertexInEdges 
            = new VertexEdgeDictionary();
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
            if (vertexCapacity > -1)
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
            get { return typeof(TVertex); }
        }

        public static Type EdgeType
        {
            get { return typeof(TEdge); }
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

        public IEnumerable<TVertex> Vertices
        {
            get { return this.vertexOutEdges.Keys; }
        }

        public bool ContainsVertex(TVertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            return this.vertexOutEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v].Count == 0;
        }

        public int OutDegree(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v].Count;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v];
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexOutEdges[v][index];
        }

        public bool IsInEdgesEmpty(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v].Count == 0;
        }

        public int InDegree(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v].Count;
        }

        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v];
        }

        public TEdge InEdge(TVertex v, int index)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.vertexInEdges[v][index];
        }

        public int Degree(TVertex v)
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

        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (EdgeList edges in this.vertexOutEdges.Values)
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

        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            GraphContracts.AssumeInVertexSet(this, source, "source");
            GraphContracts.AssumeInVertexSet(this, target, "target");

            EdgeList edgeList;
            if (this.vertexOutEdges.TryGetValue(source, out edgeList) &&
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

            EdgeList edgeList;
            if (this.vertexOutEdges.TryGetValue(source, out edgeList))
            {
                List<TEdge> list = new List<TEdge>(edgeList.Count);
                foreach (var edge in edgeList)
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

        public bool ContainsEdge(TEdge edge)
        {
            GraphContracts.AssumeInVertexSet(this, edge, "edge");
            return this.vertexOutEdges[edge.Source].Contains(edge);
        }

        public virtual void AddVertex(TVertex v)
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

            // collect edges to remove
            EdgeList edgesToRemove = new EdgeList();
            foreach (var outEdge in this.OutEdges(v))
            {
                this.vertexInEdges[outEdge.Target].Remove(outEdge);
                edgesToRemove.Add(outEdge);
            }
            foreach (var inEdge in this.InEdges(v))
            {
                // might already have been removed
                if(this.vertexOutEdges[inEdge.Source].Remove(inEdge))
                    edgesToRemove.Add(inEdge);
            }

            // notify users
            if (this.EdgeRemoved != null)
            {
                foreach(TEdge edge in edgesToRemove)
                    this.OnEdgeRemoved(new EdgeEventArgs<TVertex,TEdge>(edge));
            }

            this.vertexOutEdges.Remove(v);
            this.vertexInEdges.Remove(v);
            this.edgeCount -= edgesToRemove.Count;
            this.OnVertexRemoved(new VertexEventArgs<TVertex>(v));

            GraphContracts.Assert(this.edgeCount >= 0);
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

        public virtual bool AddEdge(TEdge e)
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

            this.OnEdgeAdded(new EdgeEventArgs<TVertex, TEdge>(e));

            return true;
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            GraphContracts.AssumeNotNull(edges, "edges");
            foreach (var edge in edges)
                this.AddEdge(edge);
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

        public event EdgeEventHandler<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<TVertex, TEdge> args)
        {
            EdgeEventHandler<TVertex, TEdge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveEdge(TEdge e)
        {
            GraphContracts.AssumeInVertexSet(this, e, "e");
            if (this.vertexOutEdges[e.Source].Remove(e))
            {
                this.vertexInEdges[e.Target].Remove(e);
                this.edgeCount--;
                GraphContracts.Assert(this.edgeCount >= 0);

                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(e));
                return true;
            }
            else
            {
                return false;
            }
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
            EdgeList edges = new EdgeList();
            foreach (var edge in this.Edges)
                if (predicate(edge))
                    edges.Add(edge);

            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");
            EdgeList edges = new EdgeList();
            foreach (var edge in this.OutEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public int RemoveInEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");
            EdgeList edges = new EdgeList();
            foreach (var edge in this.InEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public void ClearOutEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            EdgeList outEdges = this.vertexOutEdges[v];
            foreach (var edge in outEdges)
            {
                this.vertexInEdges[edge.Target].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }

            this.edgeCount -= outEdges.Count;
            outEdges.Clear();
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public void ClearInEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            EdgeList inEdges = this.vertexInEdges[v];
            foreach (var edge in inEdges)
            {
                this.vertexOutEdges[edge.Source].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }

            this.edgeCount -= inEdges.Count;
            inEdges.Clear();
            GraphContracts.Assert(this.edgeCount >= 0);
        }

        public void ClearEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");

            ClearOutEdges(v);
            ClearInEdges(v);
        }

        public void TrimEdgeExcess()
        {
            foreach (var edges in this.vertexInEdges.Values)
                edges.TrimExcess();
            foreach (var edges in this.vertexOutEdges.Values)
                edges.TrimExcess();
        }

        public void Clear()
        {
            this.vertexOutEdges.Clear();
            this.vertexInEdges.Clear();
            this.edgeCount = 0;
        }

        public void MergeVertex(TVertex v, IEdgeFactory<TVertex, TEdge> edgeFactory)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(edgeFactory, "edgeFactory");

            // storing edges in local array
            EdgeList inedges = this.vertexInEdges[v];
            EdgeList outedges = this.vertexOutEdges[v];

            // remove vertex
            this.RemoveVertex(v);

            // add edges from each source to each target
            foreach (var source in inedges)
            {
                //is it a self edge
                if (source.Source.Equals(v))
                    continue;
                foreach (var target in outedges)
                {
                    if (v.Equals(target.Target))
                        continue;
                    // we add an new edge
                    this.AddEdge(edgeFactory.CreateEdge(source.Source, target.Target));
                }
            }
        }

        public void MergeVertexIf(VertexPredicate<TVertex> vertexPredicate, IEdgeFactory<TVertex, TEdge> edgeFactory)
        {
            GraphContracts.AssumeNotNull(vertexPredicate, "vertexPredicate");
            GraphContracts.AssumeNotNull(edgeFactory, "edgeFactory");

            // storing vertices to merge
            VertexList mergeVertices = new VertexList(this.VertexCount / 4);
            foreach (var v in this.Vertices)
                if (vertexPredicate(v))
                    mergeVertices.Add(v);

            // applying merge recursively
            foreach (var v in mergeVertices)
                MergeVertex(v, edgeFactory);
        }

        [Serializable]
        private sealed class VertexList : List<TVertex>
        {
            public VertexList() { }
            public VertexList(int capacity)
                : base(capacity) { }
        }

        [Serializable]
        private sealed class EdgeList : List<TEdge>, ICloneable
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
            : Dictionary<TVertex, EdgeList>, ICloneable, ISerializable
        {
            public VertexEdgeDictionary() { }
            public VertexEdgeDictionary(int capacity)
                : base(capacity)
            { }

			public VertexEdgeDictionary(SerializationInfo info, StreamingContext context):base(info,context) { }

            public VertexEdgeDictionary Clone()
            {
                VertexEdgeDictionary clone = new VertexEdgeDictionary(this.Count);
                foreach (KeyValuePair<TVertex, EdgeList> kv in this)
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

        public BidirectionalGraph<TVertex, TEdge> Clone()
        {
            return new BidirectionalGraph<TVertex, TEdge>(
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
