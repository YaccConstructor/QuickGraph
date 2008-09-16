using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public class UndirectedGraph<TVertex,TEdge> :
        IMutableUndirectedGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly bool allowParallelEdges = true;
        private readonly Dictionary<TVertex, List<TEdge>> adjacentEdges = 
            new Dictionary<TVertex, List<TEdge>>();
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

        public void AddVertex(TVertex v)
        {
            GraphContracts.AssumeNotInVertexSet(this, v, "v");
            this.adjacentEdges.Add(v, new List<TEdge>());
        }

        public bool RemoveVertex(TVertex v)
        {
            GraphContracts.AssumeNotNull(v, "v");
            this.ClearAdjacentEdges(v);
            return this.adjacentEdges.Remove(v);
        }

        public int RemoveVertexIf(VertexPredicate<TVertex> pred)
        {
            GraphContracts.AssumeNotNull(pred, "pred");
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
            GraphContracts.AssumeInVertexSet(this, v, "v");
            GraphContracts.AssumeNotNull(predicate, "predicate");

            IList<TEdge> outEdges = this.adjacentEdges[v];
            List<TEdge> edges = new List<TEdge>(outEdges.Count);
            foreach (var edge in outEdges)
                if (predicate(edge))
                    edges.Add(edge);

            this.RemoveEdges(edges);
            return edges.Count;
        }

        public void ClearAdjacentEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            IList<TEdge> edges = this.adjacentEdges[v];
            this.edgeCount -= edges.Count;
            foreach (var edge in edges)
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
            foreach(TEdge edge in this.AdjacentEdges(source))
            {
                if (edge.Source.Equals(source) && edge.Target.Equals(target))
                    return true;

                if (edge.Target.Equals(source) && edge.Source.Equals(target))
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
            GraphContracts.AssumeNotNull(vertex, "vertex");
            return this.adjacentEdges.ContainsKey(vertex);
        }
        #endregion

        #region IMutableEdgeListGraph<Vertex,Edge> Members

        public bool AddEdge(TEdge edge)
        {
            GraphContracts.AssumeInVertexSet(this, edge, "edge");

            if (!this.AllowParallelEdges)
            {
                if (this.adjacentEdges[edge.Source].Contains(edge))
                    return false;
            }
            this.adjacentEdges[edge.Source].Add(edge);
            this.adjacentEdges[edge.Target].Add(edge);
            this.edgeCount++;

            this.OnEdgeAdded(new EdgeEventArgs<TVertex, TEdge>(edge));

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
            GraphContracts.AssumeInVertexSet(this, edge, "edge");

            this.adjacentEdges[edge.Source].Remove(edge);
            if (this.adjacentEdges[edge.Target].Remove(edge))
            {
                this.edgeCount--;
                System.Diagnostics.Debug.Assert(this.edgeCount >= 0);
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
            GraphContracts.AssumeNotNull(predicate, "predicate");

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
            GraphContracts.AssumeNotNull(edges, "edges");

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
                Dictionary<TEdge, GraphColor> edgeColors = new Dictionary<TEdge, GraphColor>(this.EdgeCount);
                foreach (IList<TEdge> edges in this.adjacentEdges.Values)
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
            GraphContracts.AssumeInVertexSet(this, edge, "edge");
            foreach (var e in this.Edges)
                if (e.Equals(edge))
                    return true;
            return false;
        }
        #endregion

        #region IUndirectedGraph<Vertex,Edge> Members

        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.adjacentEdges[v];
        }

        public int AdjacentDegree(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.adjacentEdges[v].Count;
        }

        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            GraphContracts.AssumeInVertexSet(this, v, "v");
            return this.adjacentEdges[v].Count == 0;
        }

        #endregion
    }
}
