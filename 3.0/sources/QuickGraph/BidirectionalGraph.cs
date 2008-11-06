using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
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
        private readonly VertexEdgeDictionary<TVertex, TEdge> vertexOutEdges 
            = new VertexEdgeDictionary<TVertex, TEdge>();
        private readonly VertexEdgeDictionary<TVertex, TEdge> vertexInEdges 
            = new VertexEdgeDictionary<TVertex, TEdge>();
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
                this.vertexInEdges = new VertexEdgeDictionary<TVertex, TEdge>(vertexCapacity);
                this.vertexOutEdges = new VertexEdgeDictionary<TVertex, TEdge>(vertexCapacity);
            }
            else
            {
                this.vertexInEdges = new VertexEdgeDictionary<TVertex, TEdge>();
                this.vertexOutEdges = new VertexEdgeDictionary<TVertex, TEdge>();
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
            CodeContract.Requires(v != null);
            return this.vertexOutEdges.ContainsKey(v);
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexOutEdges[v].Count == 0;
        }

        public int OutDegree(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexOutEdges[v].Count;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexOutEdges[v];
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexOutEdges[v][index];
        }

        public bool IsInEdgesEmpty(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexInEdges[v].Count == 0;
        }

        public int InDegree(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexInEdges[v].Count;
        }

        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexInEdges[v];
        }

        public TEdge InEdge(TVertex v, int index)
        {
            GraphContract.RequiresInVertexSet(this, v);
            return this.vertexInEdges[v][index];
        }

        public int Degree(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);
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
                return this.edgeCount; 
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var edges in this.vertexOutEdges.Values)
                    foreach (var edge in edges)
                        yield return edge;
            }
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            GraphContract.RequiresInVertexSet(this, source);
            GraphContract.RequiresInVertexSet(this, target);
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
            GraphContract.RequiresInVertexSet(this, source);
            GraphContract.RequiresInVertexSet(this, target);

            EdgeList<TVertex, TEdge> edgeList;
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
            GraphContract.RequiresInVertexSet(this, source);
            GraphContract.RequiresInVertexSet(this, target);

            EdgeList<TVertex, TEdge> edgeList;
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
            GraphContract.RequiresInVertexSet(this, edge, "edge");
            return this.vertexOutEdges[edge.Source].Contains(edge);
        }

        public virtual void AddVertex(TVertex v)
        {
            GraphContract.RequiresNotInVertexSet(this, v, "v");

            if (this.EdgeCapacity > 0)
            {
                this.vertexOutEdges.Add(v, new EdgeList<TVertex, TEdge>(this.EdgeCapacity));
                this.vertexInEdges.Add(v, new EdgeList<TVertex, TEdge>(this.EdgeCapacity));
            }
            else
            {
                this.vertexOutEdges.Add(v, new EdgeList<TVertex, TEdge>());
                this.vertexInEdges.Add(v, new EdgeList<TVertex, TEdge>());
            }
            this.OnVertexAdded(new VertexEventArgs<TVertex>(v));
        }

        public virtual void AddVertexRange(IEnumerable<TVertex> vertices)
        {
            CodeContract.Requires(vertices != null);
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
            CodeContract.Requires(v != null);

            if (!this.ContainsVertex(v))
                return false;

            // collect edges to remove
            var edgesToRemove = new EdgeList<TVertex, TEdge>();
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
            CodeContract.Requires(predicate != null);

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
            GraphContract.RequiresInVertexSet(this, e, "e");
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
            CodeContract.Requires(edges != null);
            foreach (var edge in edges)
                this.AddEdge(edge);
        }

        public virtual bool AddVerticesAndEdge(TEdge e)
        {
            CodeContract.Requires(e != null);

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
            GraphContract.RequiresInVertexSet(this, e, "e");
            if (this.vertexOutEdges[e.Source].Remove(e))
            {
                this.vertexInEdges[e.Target].Remove(e);
                this.edgeCount--;
                CodeContract.Assert(this.edgeCount >= 0);

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
            CodeContract.Requires(predicate != null);

            var edges = new EdgeList<TVertex, TEdge>();
            foreach (var edge in this.Edges)
                if (predicate(edge))
                    edges.Add(edge);

            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            GraphContract.RequiresInVertexSet(this, v);
            CodeContract.Requires(predicate != null);

            var edges = new EdgeList<TVertex, TEdge>();
            foreach (var edge in this.OutEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public int RemoveInEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            GraphContract.RequiresInVertexSet(this, v);
            CodeContract.Requires(predicate != null);

            var edges = new EdgeList<TVertex, TEdge>();
            foreach (var edge in this.InEdges(v))
                if (predicate(edge))
                    edges.Add(edge);
            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public void ClearOutEdges(TVertex v)
        {
            CodeContract.Requires(v != null);
            GraphContract.RequiresInVertexSet(this, v);

            var outEdges = this.vertexOutEdges[v];
            foreach (var edge in outEdges)
            {
                this.vertexInEdges[edge.Target].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }

            this.edgeCount -= outEdges.Count;
            outEdges.Clear();
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            CodeContract.Invariant(this.edgeCount >= 0);            
        }

        public void ClearInEdges(TVertex v)
        {
            CodeContract.Requires(v != null);
            GraphContract.RequiresInVertexSet(this, v);

            var inEdges = this.vertexInEdges[v];
            foreach (var edge in inEdges)
            {
                this.vertexOutEdges[edge.Source].Remove(edge);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
            }

            this.edgeCount -= inEdges.Count;
            inEdges.Clear();
        }

        public void ClearEdges(TVertex v)
        {
            GraphContract.RequiresInVertexSet(this, v);

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

        public void MergeVertex(TVertex v, EdgeFactory<TVertex, TEdge> edgeFactory)
        {
            GraphContract.RequiresInVertexSet(this, v);
            CodeContract.Requires(edgeFactory != null);

            // storing edges in local array
            var inedges = this.vertexInEdges[v];
            var outedges = this.vertexOutEdges[v];

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
                    this.AddEdge(edgeFactory(source.Source, target.Target));
                }
            }
        }

        public void MergeVertexIf(VertexPredicate<TVertex> vertexPredicate, EdgeFactory<TVertex, TEdge> edgeFactory)
        {
            CodeContract.Requires(vertexPredicate != null);
            CodeContract.Requires(edgeFactory != null);

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

        #region ICloneable Members
        private BidirectionalGraph(
            VertexEdgeDictionary<TVertex, TEdge> vertexInEdges,
            VertexEdgeDictionary<TVertex, TEdge> vertexOutEdges,
            int edgeCount,
            int edgeCapacity,
            bool allowParallelEdges
            )
        {
            CodeContract.Requires(vertexInEdges != null);
            CodeContract.Requires(vertexOutEdges != null);
            CodeContract.Requires(edgeCount >= 0);

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
