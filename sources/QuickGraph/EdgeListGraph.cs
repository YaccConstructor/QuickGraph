using System;
using System.Collections.Generic;

namespace QuickGraph
{
    [Serializable]
    public class EdgeListGraph<TVertex,TEdge> :
        IEdgeListGraph<TVertex,TEdge>,
        IMutableEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly bool isDirected = true;
        private readonly bool allowParralelEdges = true;
        private readonly EdgeEdgeDictionary edges = new EdgeEdgeDictionary();

        [Serializable]
        public class EdgeEdgeDictionary : Dictionary<TEdge, TEdge>
        { }

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

        public bool AddEdge(TEdge edge)
        {
            GraphContracts.AssumeNotNull(edge, "edge");
            if(this.ContainsEdge(edge))
                return false;
            this.edges.Add(edge, edge);
            this.OnEdgeAdded(new EdgeEventArgs<TVertex,TEdge>(edge));
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
    }
}
