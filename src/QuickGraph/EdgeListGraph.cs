using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("EdgeCount = {EdgeCount}")]
    public class EdgeListGraph<TVertex, TEdge>
        : IEdgeListGraph<TVertex,TEdge>
        , IMutableEdgeListGraph<TVertex,TEdge>
#if !SILVERLIGHT
        , ICloneable
#endif
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

        [Pure]
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
            return this.AddEdge(edge);
        }

        public int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddVerticesAndEdge(edge))
                    count++;
            return count;
        }

        public bool AddEdge(TEdge edge)
        {
            if(this.ContainsEdge(edge))
                return false;
            this.edges.Add(edge, edge);
            this.OnEdgeAdded(edge);
            return true;
        }

        public int AddEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddEdge(edge))
                    count++;
            return count;
        }

        public event EdgeAction<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(TEdge args)
        {
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(args);
        }

        public bool RemoveEdge(TEdge edge)
        {
            if (this.edges.Remove(edge))
            {
                this.OnEdgeRemoved(edge);
                return true;
            }
            else
                return false;
        }

        public event EdgeAction<TVertex, TEdge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(TEdge args)
        {
            var eh = this.EdgeRemoved;
            if (eh != null)
                eh(args);
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
            var edges = this.edges.Clone();
            this.edges.Clear();
            foreach (var edge in edges.Keys)
                this.OnEdgeRemoved(edge);
            this.OnCleared(EventArgs.Empty);
        }

        public event EventHandler Cleared;
        private void OnCleared(EventArgs e)
        {
            var eh = this.Cleared;
            if (eh != null)
                eh(this, e);
        }

        #region ICloneable Members
        private EdgeListGraph(
            bool isDirected,
            bool allowParralelEdges,
            EdgeEdgeDictionary<TVertex, TEdge> edges)
        {
            Contract.Requires(edges != null);

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

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif
        #endregion

        #region IVertexSet<TVertex> Members
        [Pure]
        public bool IsVerticesEmpty
        {
            get { return this.edges.Count == 0; }
        }

        [Pure]
        public int VertexCount
        {
            get
            {
                return this.GetVertexCounts().Count;
            }
        }

        [Pure]
        public IEnumerable<TVertex> Vertices
        {
            get
            {
                return this.GetVertexCounts().Keys;
            }
        }

        private Dictionary<TVertex, int> GetVertexCounts()
        {
            var vertices = new Dictionary<TVertex, int>(this.EdgeCount * 2);
            foreach (var e in this.Edges)
            {
                vertices[e.Source]++;
                vertices[e.Target]++;
            }
            return vertices;
        }

        [Pure]
        public bool ContainsVertex(TVertex vertex)
        {
            foreach (var e in this.Edges)
                if (e.Source.Equals(vertex) ||
                    e.Target.Equals(vertex))
                    return true;

            return false;
        }
        #endregion
    }
}
