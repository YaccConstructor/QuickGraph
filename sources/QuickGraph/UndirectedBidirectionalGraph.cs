using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    [Serializable]
    public sealed class UndirectedBidirectionalGraph<Vertex,Edge> :
        IUndirectedGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private IBidirectionalGraph<Vertex, Edge> visitedGraph;

        public UndirectedBidirectionalGraph(IBidirectionalGraph<Vertex, Edge> visitedGraph)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            this.visitedGraph = visitedGraph;
        }

        public IBidirectionalGraph<Vertex, Edge> VisitedGraph
        {
            get { return this.visitedGraph; }
        }

        #region IUndirectedGraph<Vertex,Edge> Members

        public IEnumerable<Edge> AdjacentEdges(Vertex v)
        {
            foreach (Edge e in this.VisitedGraph.OutEdges(v))
                yield return e;
            foreach (Edge e in this.VisitedGraph.InEdges(v))
            {
                // we skip selfedges here since
                // we already did those in the outedge run
                if (e.Source.Equals(e.Target))
                    continue;
                yield return e;
            }
        }

        public int AdjacentDegree(Vertex v)
        {
            return this.VisitedGraph.Degree(v);
        }

        public bool IsAdjacentEdgesEmpty(Vertex v)
        {
            return this.VisitedGraph.IsOutEdgesEmpty(v) && this.VisitedGraph.IsInEdgesEmpty(v);
        }

        public Edge AdjacentEdge(Vertex v, int index)
        {
            throw new NotSupportedException();
        }

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IVertexSet<Vertex,Edge> Members

        public bool IsVerticesEmpty
        {
            get  { return this.VisitedGraph.IsVerticesEmpty; }
        }

        public int VertexCount
        {
            get { return this.VisitedGraph.VertexCount; }
        }

        public IEnumerable<Vertex> Vertices
        {
            get { return this.VisitedGraph.Vertices; }
        }

        public bool ContainsVertex(Vertex vertex)
        {
            return this.VisitedGraph.ContainsVertex(vertex);
        }

        #endregion

        #region IEdgeListGraph<Vertex,Edge> Members

        public bool IsEdgesEmpty
        {
            get { return this.VisitedGraph.IsEdgesEmpty; }
        }

        public int EdgeCount
        {
            get { return this.VisitedGraph.EdgeCount; }
        }

        public IEnumerable<Edge> Edges
        {
            get { return this.VisitedGraph.Edges; }
        }

        public bool ContainsEdge(Edge edge)
        {
            return this.VisitedGraph.ContainsEdge(edge);
        }

        #endregion

        #region IGraph<Vertex,Edge> Members

        public bool IsDirected
        {
            get { return false; }
        }

        public bool AllowParallelEdges
        {
            get { return this.VisitedGraph.AllowParallelEdges; }
        }

        #endregion
    }
}
