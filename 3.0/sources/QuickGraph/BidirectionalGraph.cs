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
            [Pure]
            get { return typeof(TVertex); }
        }

        public static Type EdgeType
        {
            [Pure]
            get { return typeof(TEdge); }
        }

        public int EdgeCapacity
        {
            [Pure]
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
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

        public bool IsVerticesEmpty
        {
            [Pure]
            get { return this.vertexOutEdges.Count == 0; }
        }

        public int VertexCount
        {
            [Pure]
            get { return this.vertexOutEdges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            [Pure]
            get { return this.vertexOutEdges.Keys; }
        }

        [Pure]
        public bool ContainsVertex(TVertex v)
        {
            Contract.Requires(v != null);
            return this.vertexOutEdges.ContainsKey(v);
        }

        [Pure]
        public bool IsOutEdgesEmpty(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));
            
            return this.vertexOutEdges[v].Count == 0;
        }

        [Pure]
        public int OutDegree(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexOutEdges[v].Count;
        }

        [Pure]
        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexOutEdges[v];
        }

        [Pure]
        public TEdge OutEdge(TVertex v, int index)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexOutEdges[v][index];
        }

        [Pure]
        public bool IsInEdgesEmpty(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexInEdges[v].Count == 0;
        }

        [Pure]
        public int InDegree(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexInEdges[v].Count;
        }

        [Pure]
        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.vertexInEdges[v];
        }

        [Pure]
        public TEdge InEdge(TVertex v, int index)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(index >= 0 && index < this.InDegree(v));

            return this.vertexInEdges[v][index];
        }

        [Pure]
        public int Degree(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

            return this.OutDegree(v) + this.InDegree(v);
        }

        public bool IsEdgesEmpty
        {
            [Pure]
            get { return this.edgeCount == 0; }
        }

        public int EdgeCount
        {
            [Pure]
            get 
            {
                return this.edgeCount; 
            }
        }

        public IEnumerable<TEdge> Edges
        {
            [Pure]
            get
            {
                foreach (var edges in this.vertexOutEdges.Values)
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

        [Pure]
        public bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(GraphContract.InVertexSet(this, source));
            Contract.Requires(GraphContract.InVertexSet(this, target));

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

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            Contract.Requires(edge != null);
            Contract.Requires(GraphContract.InVertexSet(this, edge));

            return this.vertexOutEdges[edge.Source].Contains(edge);
        }

        public virtual void AddVertex(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(!GraphContract.InVertexSet(this, v));

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
            Contract.Requires(vertices != null);
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
            Contract.Requires(v != null);

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
            var eh = this.VertexRemoved;
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

        public virtual bool AddEdge(TEdge e)
        {
            Contract.Requires(GraphContract.InVertexSet(this, e));
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
            Contract.Requires(edges != null);
            foreach (var edge in edges)
                this.AddEdge(edge);
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

        public event EdgeEventHandler<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<TVertex, TEdge> args)
        {
            EdgeEventHandler<TVertex, TEdge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public virtual bool RemoveEdge(TEdge e)
        {
            Contract.Requires(GraphContract.InVertexSet(this, e));
            if (this.vertexOutEdges[e.Source].Remove(e))
            {
                this.vertexInEdges[e.Target].Remove(e);
                this.edgeCount--;
                Contract.Assert(this.edgeCount >= 0);

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
            Contract.Requires(predicate != null);

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
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(predicate != null);

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
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(predicate != null);

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
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

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
            Contract.Invariant(this.edgeCount >= 0);            
        }

        public void ClearInEdges(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));

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
            Contract.Requires(GraphContract.InVertexSet(this, v));

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
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(edgeFactory != null);

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
            Contract.Requires(vertexPredicate != null);
            Contract.Requires(edgeFactory != null);

            // storing vertices to merge
            var mergeVertices = new VertexList<TVertex>(this.VertexCount / 4);
            foreach (var v in this.Vertices)
                if (vertexPredicate(v))
                    mergeVertices.Add(v);

            // applying merge recursively
            foreach (var v in mergeVertices)
                MergeVertex(v, edgeFactory);
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
            Contract.Requires(vertexInEdges != null);
            Contract.Requires(vertexOutEdges != null);
            Contract.Requires(edgeCount >= 0);

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
