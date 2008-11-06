using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("EdgeCount = {EdgeCount}")]
    public class EdgeListGraph<TVertex, TEdge>
        : IEdgeListGraph<TVertex,TEdge>
        , IMutableEdgeListGraph<TVertex,TEdge>
        , ICloneable
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParralelEdges = true;
        private readonly EdgeEdgeDictionary<TVertex, TEdge> edges = new EdgeEdgeDictionary<TVertex, TEdge>();

        public EdgeListGraph()
        {}

        public EdgeListGraph(bool isDirected, bool allowParralelEdges)
        {
            this.isDirected = isDirected;
            this.allowParralelEdges = allowParralelEdges;
        }

        public bool IsEdgesEmpty
        {
            get 
            { 
                return this.edges.Count==0;
            }
        }

        public int EdgeCount
        {
            get 
            { 
                return this.edges.Count;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get 
            { 
                return this.edges.Keys;
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            return this.edges.ContainsKey(edge);
        }

        public bool IsDirected
        {
            get 
            { 
                return this.isDirected;
            }
        }

        public bool AllowParallelEdges
        {
            get 
            { 
                return this.allowParralelEdges;
            }
        }

        public bool AddVerticesAndEdge(TEdge edge)
        {
            CodeContract.Requires(edge != null);

            return this.AddEdge(edge);
        }

        public bool AddEdge(TEdge edge)
        {
            CodeContract.Requires(edge != null);

            if(this.ContainsEdge(edge))
                return false;
            this.edges.Add(edge, edge);
            this.OnEdgeAdded(new EdgeEventArgs<TVertex,TEdge>(edge));
            return true;
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            CodeContract.Requires(edges != null);

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
            if (this.edges.Remove(edge))
            {
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
            List<TEdge> edgesToRemove = new List<TEdge>();
            foreach (var edge in this.Edges)
                if (predicate(edge))
                    edgesToRemove.Add(edge);

            foreach (var edge in edgesToRemove)
                edges.Remove(edge);
            return edgesToRemove.Count;
        }

        public void Clear()
        {
            this.edges.Clear();
        }

        #region ICloneable Members
        private EdgeListGraph(
            bool isDirected,
            bool allowParralelEdges,
            EdgeEdgeDictionary<TVertex, TEdge> edges)
        {
            CodeContract.Requires(edges != null);

            this.isDirected = isDirected;
            this.allowParralelEdges = allowParralelEdges;
            this.edges = edges;
        }

        public EdgeListGraph<TVertex, TEdge> Clone()
        {
            return new EdgeListGraph<TVertex, TEdge>(
                this.isDirected, 
                this.allowParralelEdges, 
                this.edges.Clone()
                );
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion
    }
}
