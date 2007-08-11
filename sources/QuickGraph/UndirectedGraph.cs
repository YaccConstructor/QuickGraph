using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public class UndirectedGraph<Vertex,Edge> :
        IMutableUndirectedGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private readonly bool allowParallelEdges = true;
        private readonly Dictionary<Vertex, IList<Edge> > adjacentEdges = new Dictionary<Vertex, IList<Edge>>();
        private int edgeCount = 0;

        public UndirectedGraph()
            :this(true)
        {}

        public UndirectedGraph(bool allowParallelEdges)
        {
            this.allowParallelEdges = allowParallelEdges;
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

        public void AddVertex(Vertex v)
        {
            this.adjacentEdges.Add(v, new List<Edge>());
        }

        public bool RemoveVertex(Vertex v)
        {
            this.ClearAdjacentEdges(v);
            return this.adjacentEdges.Remove(v);
        }

        public int RemoveVertexIf(VertexPredicate<Vertex> pred)
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach (Vertex v in this.Vertices)
                if (pred(v))
                    vertices.Add(v);

            foreach (Vertex v in vertices)
                RemoveVertex(v);
            return vertices.Count;
        }
        #endregion

        #region IMutableIncidenceGraph<Vertex,Edge> Members
        public int RemoveAdjacentEdgeIf(Vertex v, EdgePredicate<Vertex, Edge> predicate)
        {
            IList<Edge> outEdges = this.adjacentEdges[v];
            List<Edge> edges = new List<Edge>(outEdges.Count);
            foreach (Edge edge in outEdges)
                if (predicate(edge))
                    edges.Add(edge);

            this.RemoveEdges(edges);
            return edges.Count;
        }

        public void ClearAdjacentEdges(Vertex v)
        {
            IList<Edge> edges = this.adjacentEdges[v];
            this.edgeCount -= edges.Count;
            foreach (Edge edge in edges)
            {
                if (edge.Source.Equals(v))
                    this.adjacentEdges[edge.Target].Remove(edge);
                else
                    this.adjacentEdges[edge.Source].Remove(edge);
            }
            System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
        }
        #endregion

        #region IMutableGraph<Vertex,Edge> Members

        public void Clear()
        {
            this.adjacentEdges.Clear();
            this.edgeCount = 0;
        }
        #endregion

        #region IUndirectedGraph<Vertex,Edge> Members

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            foreach(Edge edge in this.AdjacentEdges(source))
            {
                if (edge.Source.Equals(source) && edge.Target.Equals(target))
                    return true;

                if (edge.Target.Equals(source) && edge.Source.Equals(target))
                    return true;
            }
            return false;
        }

        public Edge AdjacentEdge(Vertex v, int index)
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

        public IEnumerable<Vertex> Vertices
        {
            get { return this.adjacentEdges.Keys; }
        }


        public bool ContainsVertex(Vertex vertex)
        {
            return this.adjacentEdges.ContainsKey(vertex);
        }
        #endregion

        #region IMutableEdgeListGraph<Vertex,Edge> Members

        public bool AddEdge(Edge edge)
        {
            if (edge == null)
                throw new ArgumentNullException("edge");

            if (!this.AllowParallelEdges)
            {
                if (this.adjacentEdges[edge.Source].Contains(edge))
                    return false;
            }
            this.adjacentEdges[edge.Source].Add(edge);
            this.adjacentEdges[edge.Target].Add(edge);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<Vertex, Edge>(edge));

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
            if (edge == null)
                throw new ArgumentNullException("edge");
            this.adjacentEdges[edge.Source].Remove(edge);
            if (this.adjacentEdges[edge.Target].Remove(edge))
            {
                this.edgeCount--;
                System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
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

        public int RemoveEdgeIf(EdgePredicate<Vertex, Edge> predicate)
        {
            List<Edge> edges = new List<Edge>();
            foreach (Edge edge in this.Edges)
            {
                if (predicate(edge))
                    edges.Add(edge);
            }
            return this.RemoveEdges(edges);
        }

        public int RemoveEdges(IEnumerable<Edge> edges)
        {
            int count = 0;
            foreach (Edge edge in edges)
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

        public IEnumerable<Edge> Edges
        {
            get 
            {
                Dictionary<Edge, GraphColor> edgeColors = new Dictionary<Edge, GraphColor>(this.EdgeCount);
                foreach (IList<Edge> edges in this.adjacentEdges.Values)
                {
                    foreach(Edge edge in edges)
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

        public bool ContainsEdge(Edge edge)
        {
            foreach (Edge e in this.Edges)
                if (e.Equals(edge))
                    return true;
            return false;
        }
        #endregion

        #region IUndirectedGraph<Vertex,Edge> Members

        public IEnumerable<Edge> AdjacentEdges(Vertex v)
        {
            return this.adjacentEdges[v];
        }

        public int AdjacentDegree(Vertex v)
        {
            return this.adjacentEdges[v].Count;
        }

        public bool IsAdjacentEdgesEmpty(Vertex v)
        {
            return this.adjacentEdges[v].Count == 0;
        }

        #endregion
    }
}
