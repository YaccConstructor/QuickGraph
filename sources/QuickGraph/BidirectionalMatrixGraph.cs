using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public class BidirectionalMatrixGraph<Edge> :
        IBidirectionalGraph<int, Edge>,
        IMutableEdgeListGraph<int, Edge>
        where Edge : IEdge<int>
    {
        private int vertexCount;
        private int edgeCount;
        private Edge[,] edges;

        public BidirectionalMatrixGraph(int vertexCount)
        {
            this.vertexCount = vertexCount;
            this.edgeCount = 0;
            this.edges = new Edge[vertexCount, vertexCount];
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

        public IEnumerable<Edge> Edges
        {
            get
            {
                for (int i = 0; i < this.VertexCount; ++i)
                {
                    for (int j = 0; j < this.VertexCount; ++j)
                    {
                        Edge e = this.edges[i, j];
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

        public IEnumerable<Edge> InEdges(int v)
        {
            for (int i = 0; i < this.VertexCount; ++i)
            {
                Edge e = this.edges[i, v];
                if (e != null)
                    yield return e;
            }
        }

        public Edge InEdge(int v, int index)
        {
            int count = 0;
            for (int i = 0; i < this.VertexCount; ++i)
            {
                Edge e = this.edges[i, v];
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

        public IEnumerable<Edge> OutEdges(int v)
        {
            for (int j = 0; j < this.vertexCount; ++j)
            {
                Edge e = this.edges[v, j];
                if (e != null)
                    yield return e;
            }
        }

        public Edge OutEdge(int v, int index)
        {
            int count = 0;
            for (int j = 0; j < this.vertexCount; ++j)
            {
                Edge e = this.edges[v, j];
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

        public bool ContainsEdge(Edge edge)
        {
            Edge e = this.edges[edge.Source, edge.Target];
            return e!=null && e.Equals(edge);
        }

        #endregion

        #region IMutableBidirectionalGraph<int,Edge> Members

        public int RemoveInEdgeIf(int v, IEdgePredicate<int, Edge> edgePredicate)
        {
            int count = 0;
            for (int i = 0; i < this.VertexCount; ++i)
            {
                Edge e = this.edges[i, v];
                if (e != null && edgePredicate.Test(e))
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
                Edge e = this.edges[i, v];
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

        public int RemoveOutEdgeIf(int v, IEdgePredicate<int, Edge> predicate)
        {
            int count = 0;
            for (int j = 0; j < this.VertexCount; ++j)
            {
                Edge e = this.edges[v, j];
                if (e != null && predicate.Test(e))
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
                Edge e = this.edges[v, j];
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

        public bool AddEdge(Edge edge)
        {
            if (this.edges[edge.Source, edge.Target]!=null)
                throw new ParallelEdgeNotAllowedException();
            this.edges[edge.Source,edge.Target] = edge;
            this.edgeCount++;
            this.OnEdgeAdded(new EdgeEventArgs<int, Edge>(edge));
            return true;
        }

        public event EdgeEventHandler<int, Edge> EdgeAdded;
        protected virtual void OnEdgeAdded(EdgeEventArgs<int, Edge> args)
        {
            EdgeEventHandler<int, Edge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, args);
        }

        public bool RemoveEdge(Edge edge)
        {
            Edge e = this.edges[edge.Source, edge.Target];
            this.edges[edge.Source, edge.Target] = default(Edge);
            if (!e.Equals(default(Edge)))
            {
                this.edgeCount--;
                this.OnEdgeRemoved(new EdgeEventArgs<int, Edge>(edge));
                return true;
            }
            else
                return false;
        }

        public event EdgeEventHandler<int, Edge> EdgeRemoved;
        protected virtual void OnEdgeRemoved(EdgeEventArgs<int, Edge> args)
        {
            EdgeEventHandler<int, Edge> eh = this.EdgeRemoved;
            if (eh != null)
                eh(this, args);
        }

        public int RemoveEdgeIf(IEdgePredicate<int, Edge> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
}
}
