using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public sealed class UndirectedBidirectionalGraph<TVertex, TEdge> :
        IUndirectedGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IBidirectionalGraph<TVertex, TEdge> visitedGraph;

        public UndirectedBidirectionalGraph(IBidirectionalGraph<TVertex, TEdge> visitedGraph)
        {
            Contract.Requires(visitedGraph != null);

            this.visitedGraph = visitedGraph;
        }

        public IBidirectionalGraph<TVertex, TEdge> VisitedGraph
        {
            get { return this.visitedGraph; }
        }

        #region IUndirectedGraph<Vertex,Edge> Members

        [Pure]
        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            foreach (var e in this.VisitedGraph.OutEdges(v))
                yield return e;
            foreach (var e in this.VisitedGraph.InEdges(v))
            {
                // we skip selfedges here since
                // we already did those in the outedge run
                if (e.Source.Equals(e.Target))
                    continue;
                yield return e;
            }
        }

        [Pure]
        public int AdjacentDegree(TVertex v)
        {
            return this.VisitedGraph.Degree(v);
        }

        [Pure]
        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            return this.VisitedGraph.IsOutEdgesEmpty(v) && this.VisitedGraph.IsInEdgesEmpty(v);
        }

        [Pure]
        public TEdge AdjacentEdge(TVertex v, int index)
        {
            throw new NotSupportedException();
        }

        [Pure]
        public bool ContainsEdge(TVertex source, TVertex target)
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

        public IEnumerable<TVertex> Vertices
        {
            get { return this.VisitedGraph.Vertices; }
        }

        [Pure]
        public bool ContainsVertex(TVertex vertex)
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

        public IEnumerable<TEdge> Edges
        {
            get { return this.VisitedGraph.Edges; }
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
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
