using System;
using System.Collections.Generic;

namespace QuickGraph
{
    [Serializable]
    public class EdgeListGraph<Vertex,Edge> :
        IEdgeListGraph<Vertex,Edge>,
        IMutableEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private bool isDirected = true;
        private bool allowParralelEdges = true;
        private IDictionary<Edge, Edge> edges = new Dictionary<Edge, Edge>();

        public EdgeListGraph()
        {}

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

        public IEnumerable<Edge> Edges
        {
            get 
            { 
                return this.edges.Keys;
            }
        }


        public bool ContainsEdge(Edge edge)
        {
            return this.edges.ContainsKey(edge);
        }

        public bool IsDirected
        {
            get 
            { 
                return this.isDirected;
            }
            set 
            {
                this.isDirected = value;
            }
        }

        public bool AllowParallelEdges
        {
            get 
            { 
                return this.allowParralelEdges;
            }
            set 
            {
                this.allowParralelEdges = value;
            }
        }

        public bool AddEdge(Edge edge)
        {
            if(this.ContainsEdge(edge))
                return false;
            this.edges.Add(edge, edge);
            this.OnEdgeAdded(new EdgeEventArgs<Vertex,Edge>(edge));
            return true;
        }

        public event EdgeEventHandler<Vertex, Edge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<Vertex, Edge> args)
        {
            EdgeEventHandler<Vertex, Edge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public bool RemoveEdge(Edge edge)
        {
            if (this.edges.Remove(edge))
            {
                this.OnEdgeRemoved(new EdgeEventArgs<Vertex, Edge>(edge));
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
            List<Edge> edgesToRemove = new List<Edge>();
            foreach (Edge edge in this.Edges)
                if (predicate.Test(edge))
                    edgesToRemove.Add(edge);

            foreach (Edge edge in edgesToRemove)
                edges.Remove(edge);
            return edgesToRemove.Count;
        }

        public void Clear()
        {
            this.edges.Clear();
        }
    }
}
