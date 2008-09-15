using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public class BidirectionalMatrixGraph<TEdge> :
        IBidirectionalGraph<int, TEdge>,
        IMutableEdgeListGraph<int, TEdge>
        where TEdge : IEdge<int>
    {
        private readonly int vertexCount;
        private int edgeCount;
        private readonly TEdge[,] edges;

        public BidirectionalMatrixGraph(int vertexCount)        
        {
            if (vertexCount < 1)
                throw new ArgumentOutOfRangeException("vertexCount");
            this.vertexCount = vertexCount;
            this.edgeCount = 0;
            this.edges = new TEdge[vertexCount, vertexCount];
        }

        #region IGraph
        public bool AllowParallelEdges
        {
            get { return false; }
        }

        public bool IsDirected
        {
            get { return true; }
        }
        #endregion

        #region IVertexListGraph
        public int VertexCount
        {
            get { return this.vertexCount; }
        }

        public bool IsVerticesEmpty
        {
            get { return this.VertexCount == 0; }
        }
        #endregion

        #region IEdgeListGraph
        public int EdgeCount
        {
            get { return this.edgeCount; }
        }

        public bool IsEdgesEmpty
        {
            get { return this.EdgeCount == 0; }
        }

        public IEnumerable<TEdge> Edges
        {
            get
            {
                for (int i = 0; i < this.VertexCount; ++i)
                {
                    for (int j = 0; j < this.VertexCount; ++j)
                    {
                        TEdge e = this.edges[i, j];
                        if (e != null)
                            yield return e;
                    }
                }
            }
        }
        #endregion

        #region IBidirectionalGraph<int,Edge> Members

        public bool IsInEdgesEmpty(int v)
        {
            for (int i = 0; i < this.VertexCount; ++i)
                if (this.edges[i, v] != null)
                    return false;
            return true;
        }

        public int InDegree(int v)
        {
            int count = 0;
            for (int i = 0; i < this.VertexCount; ++i)
                if (this.edges[i, v] != null)
                    count++;
            return count;
        }

        public IEnumerable<TEdge> InEdges(int v)
        {
            for (int i = 0; i < this.VertexCount; ++i)
            {
                TEdge e = this.edges[i, v];
                if (e != null)
                    yield return e;
            }
        }

        public TEdge InEdge(int v, int index)
        {
            int count = 0;
            for (int i = 0; i < this.VertexCount; ++i)
            {
                TEdge e = this.edges[i, v];
                if (e != null)
                {
                    if (count == index)
                        return e;
                    count++;
                }
            }
            throw new ArgumentOutOfRangeException("index");
        }

        public int Degree(int v)
        {
            return this.InDegree(v) + this.OutDegree(v);
        }

        #endregion

        #region IIncidenceGraph<int,Edge> Members

        public bool ContainsEdge(int source, int target)
        {
            return this.edges[source, target] != null;
        }

        public bool TryGetEdge(int source, int target, out TEdge edge)
        {
            edge = this.edges[source, target];
            return edge != null;
        }

        public bool TryGetEdges(int source, int target, out IEnumerable<TEdge> edges)
        {
            TEdge edge;
            if (this.TryGetEdge(source, target, out edge))
            {
                edges = new TEdge[] { edge };
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        #endregion

        #region IImplicitGraph<int,Edge> Members

        public bool IsOutEdgesEmpty(int v)
        {
            for (int j = 0; j < this.vertexCount; ++j)
                if (this.edges[v, j] != null)
                    return false;
            return true;
        }

        public int OutDegree(int v)
        {
            int count = 0;
            for (int j = 0; j < this.vertexCount; ++j)
                if (this.edges[v, j] != null)
                    count++;
            return count;
        }

        public IEnumerable<TEdge> OutEdges(int v)
        {
            for (int j = 0; j < this.vertexCount; ++j)
            {
                TEdge e = this.edges[v, j];
                if (e != null)
                    yield return e;
            }
        }

        public TEdge OutEdge(int v, int index)
        {
            int count = 0;
            for (int j = 0; j < this.vertexCount; ++j)
            {
                TEdge e = this.edges[v, j];
                if (e != null)
                {
                    if (count==index)
                        return e;
                    count++;
                }
            }
            throw new ArgumentOutOfRangeException("index");
        }

        #endregion

        #region IVertexSet<int,Edge> Members

        public IEnumerable<int> Vertices
        {
            get 
            {
                for (int i = 0; i < this.VertexCount; ++i)
                    yield return i;
            }
        }

        public bool ContainsVertex(int vertex)
        {
            return vertex >= 0 && vertex < this.VertexCount;
        }

        #endregion

        #region IEdgeListGraph<int,Edge> Members

        public bool ContainsEdge(TEdge edge)
        {
            TEdge e = this.edges[edge.Source, edge.Target];
            return e!=null && 
                e.Equals(edge);
        }

        #endregion

        #region IMutableBidirectionalGraph<int,Edge> Members

        public int RemoveInEdgeIf(int v, EdgePredicate<int, TEdge> edgePredicate)
        {
            int count = 0;
            for (int i = 0; i < this.VertexCount; ++i)
            {
                TEdge e = this.edges[i, v];
                if (e != null && edgePredicate(e))
                {
                    this.RemoveEdge(e);
                    count++;
                }
            }
            return count;
        }

        public void ClearInEdges(int v)
        {
            for (int i = 0; i < this.VertexCount; ++i)
            {
                TEdge e = this.edges[i, v];
                if (e != null)
                    this.RemoveEdge(e);
            }
        }

        public void ClearEdges(int v)
        {
            this.ClearInEdges(v);
            this.ClearOutEdges(v);
        }

        #endregion

        #region IMutableIncidenceGraph<int,Edge> Members

        public int RemoveOutEdgeIf(int v, EdgePredicate<int, TEdge> predicate)
        {
            int count = 0;
            for (int j = 0; j < this.VertexCount; ++j)
            {
                TEdge e = this.edges[v, j];
                if (e != null && predicate(e))
                {
                    this.RemoveEdge(e);
                    count++;
                }
            }
            return count;
        }

        public void ClearOutEdges(int v)
        {
            for (int j = 0; j < this.VertexCount; ++j)
            {
                TEdge e = this.edges[v, j];
                if (e != null)
                    this.RemoveEdge(e);
            }
        }

        #endregion

        #region IMutableGraph<int,Edge> Members

        public void Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IMutableEdgeListGraph<int,Edge> Members

        public bool AddEdge(TEdge edge)
        {
            GraphContracts.AssumeNotNull(edge, "edge");
            if (this.edges[edge.Source, edge.Target]!=null)
                throw new ParallelEdgeNotAllowedException();
            this.edges[edge.Source,edge.Target] = edge;
            this.edgeCount++;
            this.OnEdgeAdded(new EdgeEventArgs<int, TEdge>(edge));
            return true;
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            GraphContracts.AssumeNotNull(edges, "edges");
            foreach (var edge in edges)
                this.AddEdge(edge);
        }

        public event EdgeEventHandler<int, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<int, TEdge> args)
        {
            EdgeEventHandler<int, TEdge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public bool RemoveEdge(TEdge edge)
        {
            TEdge e = this.edges[edge.Source, edge.Target];
            this.edges[edge.Source, edge.Target] = default(TEdge);
            if (!e.Equals(default(TEdge)))
            {
                this.edgeCount--;
                this.OnEdgeRemoved(new EdgeEventArgs<int, TEdge>(edge));
                return true;
            }
            else
                return false;
        }

        public event EdgeEventHandler<int, TEdge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(EdgeEventArgs<int, TEdge> args)
        {
            EdgeEventHandler<int, TEdge> eh = this.EdgeRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveEdgeIf(EdgePredicate<int, TEdge> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion
}
}
