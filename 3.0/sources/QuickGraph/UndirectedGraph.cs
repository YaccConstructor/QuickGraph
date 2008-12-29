using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class UndirectedGraph<TVertex, TEdge> 
        : IMutableUndirectedGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly bool allowParallelEdges = true;
        private readonly VertexEdgeDictionary<TVertex, TEdge> adjacentEdges =
            new VertexEdgeDictionary<TVertex, TEdge>();
        private int edgeCount = 0;
        private int edgeCapacity = 4;

        public UndirectedGraph()
            :this(true)
        {}

        public UndirectedGraph(bool allowParallelEdges)
        {
            this.allowParallelEdges = allowParallelEdges;
        }

        public int EdgeCapacity
        {
            get { return this.edgeCapacity; }
            set { this.edgeCapacity = value; }
        }
    
        #region IGraph<Vertex,Edge> Members
        public bool  IsDirected
        {
        	get { return false; }
        }

        public bool  AllowParallelEdges
        {
        	get { return this.allowParallelEdges; }
        }
        #endregion

        #region IMutableUndirected<Vertex,Edge> Members
        public event VertexEventHandler<TVertex> VertexAdded;
        protected virtual void OnVertexAdded(VertexEventArgs<TVertex> args)
        {
            Contract.Requires(args != null);

            var eh = this.VertexAdded;
            if (eh != null)
                eh(this, args);
        }

        public void AddVertexRange(IEnumerable<TVertex> vertices)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(Contract.ForAll(vertices, v => v != null));

            foreach (var v in vertices)
                this.AddVertex(v);
        }

        public void AddVertex(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(!GraphContract.InVertexSet(this, v));

            var edges = this.EdgeCapacity < 0 
                ? new EdgeList<TVertex, TEdge>() 
                : new EdgeList<TVertex, TEdge>(this.EdgeCapacity);
            this.adjacentEdges.Add(v, edges);
            this.OnVertexAdded(new VertexEventArgs<TVertex>(v));
        }

        private List<TEdge> AddAndReturnEdges(TVertex v)
        {
            EdgeList<TVertex, TEdge> edges;
            if (!this.adjacentEdges.TryGetValue(v, out edges))
                this.adjacentEdges[v] = edges = this.EdgeCapacity < 0 
                    ? new EdgeList<TVertex, TEdge>() 
                    : new EdgeList<TVertex, TEdge>(this.EdgeCapacity);

            return edges;
        }

        public event VertexEventHandler<TVertex> VertexRemoved;
        protected virtual void OnVertexRemoved(VertexEventArgs<TVertex> args)
        {
            Contract.Requires(args != null);

            var eh = this.VertexRemoved;
            if (eh != null)
                eh(this, args);
        }

        public bool RemoveVertex(TVertex v)
        {
            Contract.Requires(v != null);
            this.ClearAdjacentEdges(v);
            bool result = this.adjacentEdges.Remove(v);

            if (result)
                this.OnVertexRemoved(new VertexEventArgs<TVertex>(v));

            return result;
        }

        public int RemoveVertexIf(VertexPredicate<TVertex> pred)
        {
            Contract.Requires(pred != null);
            List<TVertex> vertices = new List<TVertex>();
            foreach (var v in this.Vertices)
                if (pred(v))
                    vertices.Add(v);

            foreach (var v in vertices)
                RemoveVertex(v);
            return vertices.Count;
        }
        #endregion

        #region IMutableIncidenceGraph<Vertex,Edge> Members
        public int RemoveAdjacentEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Requires(predicate != null);

            var outEdges = this.adjacentEdges[v];
            var edges = new List<TEdge>(outEdges.Count);
            foreach (var edge in outEdges)
                if (predicate(edge))
                    edges.Add(edge);

            this.RemoveEdges(edges);
            return edges.Count;
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.edgeCount >= 0);
        }

        public void ClearAdjacentEdges(TVertex v)
        {
            Contract.Requires(v != null);
            Contract.Requires(GraphContract.InVertexSet(this, v));
            Contract.Ensures(Contract.OldValue(this.edgeCount) == this.edgeCount - this.adjacentEdges[v].Count);

            var edges = this.adjacentEdges[v];
            this.edgeCount -= edges.Count;
            foreach (var edge in edges)
            {
                EdgeList<TVertex, TEdge> aEdges;
                if (this.adjacentEdges.TryGetValue(edge.Target, out aEdges))
                    aEdges.Remove(edge);
                if (this.adjacentEdges.TryGetValue(edge.Source, out aEdges))
                    aEdges.Remove(edge);
            }
        }
        #endregion

        #region IMutableGraph<Vertex,Edge> Members
        public void TrimEdgeExcess()
        {
            foreach (var edges in this.adjacentEdges.Values)
                edges.TrimExcess();
        }

        public void Clear()
        {
            this.adjacentEdges.Clear();
            this.edgeCount = 0;
        }
        #endregion

        #region IUndirectedGraph<Vertex,Edge> Members

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(GraphContract.InVertexSet(this, source));

            foreach(var edge in this.AdjacentEdges(source))
            {
                if ((edge.Source.Equals(source) && edge.Target.Equals(target)) ||
                    (edge.Target.Equals(source) && edge.Source.Equals(target)))
                    return true;
            }
            return false;
        }

        public TEdge AdjacentEdge(TVertex v, int index)
        {
            return this.adjacentEdges[v][index];
        }

        public bool IsVerticesEmpty
        {
            get { return this.adjacentEdges.Count == 0; }
        }

        public int VertexCount
        {
            get { return this.adjacentEdges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return this.adjacentEdges.Keys; }
        }


        public bool ContainsVertex(TVertex vertex)
        {
            Contract.Requires(vertex != null);
            return this.adjacentEdges.ContainsKey(vertex);
        }
        #endregion

        #region IMutableEdgeListGraph<Vertex,Edge> Members
        public bool AddVerticesAndEdge(TEdge edge)
        {
            Contract.Requires(edge != null);

            var sourceEdges = this.AddAndReturnEdges(edge.Source);
            var targetEdges = this.AddAndReturnEdges(edge.Target);

            if (!this.AllowParallelEdges)
            {
                if (ContainsEdge(sourceEdges, edge))
                    return false;
            }

            sourceEdges.Add(edge);
            targetEdges.Add(edge);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<TVertex, TEdge>(edge));

            return true;
        }

        public bool AddEdge(TEdge edge)
        {
            Contract.Requires(GraphContract.InVertexSet(this, edge));

            var sourceEdges = this.adjacentEdges[edge.Source];
            if (!this.AllowParallelEdges)
            {
                if (ContainsEdge(sourceEdges, edge))
                    return false;
            }
            var targetEdges = this.adjacentEdges[edge.Target];

            sourceEdges.Add(edge);
            targetEdges.Add(edge);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<TVertex, TEdge>(edge));

            return true;
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            Contract.Requires(edges != null);

            foreach (var edge in edges)
                this.AddEdge(edge);
        }

        public event EdgeEventHandler<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<TVertex, TEdge> args)
        {
            EdgeEventHandler<TVertex, TEdge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public bool RemoveEdge(TEdge edge)
        {
            Contract.Requires(GraphContract.InVertexSet(this, edge));

            this.adjacentEdges[edge.Source].Remove(edge);
            if (this.adjacentEdges[edge.Target].Remove(edge))
            {
                this.edgeCount--;
                Contract.Assert(this.edgeCount >= 0);
                this.OnEdgeRemoved(new EdgeEventArgs<TVertex, TEdge>(edge));
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

            List<TEdge> edges = new List<TEdge>();
            foreach (var edge in this.Edges)
            {
                if (predicate(edge))
                    edges.Add(edge);
            }
            return this.RemoveEdges(edges);
        }

        public int RemoveEdges(IEnumerable<TEdge> edges)
        {
            Contract.Requires(edges != null);

            int count = 0;
            foreach (var edge in edges)
            {
                if (RemoveEdge(edge))
                    count++;
            }
            return count;
        }
        #endregion

        #region IEdgeListGraph<Vertex,Edge> Members
        public bool IsEdgesEmpty
        {
            get { return this.EdgeCount==0; }
        }

        public int EdgeCount
        {
            get { return this.edgeCount; }
        }

        public IEnumerable<TEdge> Edges
        {
            get 
            {
                var edgeColors = new Dictionary<TEdge, GraphColor>(this.EdgeCount);
                foreach (var edges in this.adjacentEdges.Values)
                {
                    foreach(TEdge edge in edges)
                    {
                        GraphColor c;
                        if (edgeColors.TryGetValue(edge, out c))
                            continue;
                        edgeColors.Add(edge, GraphColor.Black);
                        yield return edge;
                    }
                }
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            Contract.Requires(GraphContract.InVertexSet(this, edge));
            var edges = this.Edges;
            return ContainsEdge(edges, edge);
        }

        private static bool ContainsEdge(IEnumerable<TEdge> edges, TEdge edge)
        {
            Contract.Requires(edges != null);
            Contract.Requires(edge != null);

            foreach (var e in edges)
                if (e.Source.Equals(edge.Source) && e.Target.Equals(edge.Target) ||
                    e.Target.Equals(edge.Source) && e.Source.Equals(edge.Target))
                    return true;
            return false;
        }
        #endregion

        #region IUndirectedGraph<Vertex,Edge> Members

        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            Contract.Requires(GraphContract.InVertexSet(this, v));
            return this.adjacentEdges[v];
        }

        public int AdjacentDegree(TVertex v)
        {
            Contract.Requires(GraphContract.InVertexSet(this, v));
            return this.adjacentEdges[v].Count;
        }

        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            Contract.Requires(GraphContract.InVertexSet(this, v));
            return this.adjacentEdges[v].Count == 0;
        }

        #endregion
    }
}
