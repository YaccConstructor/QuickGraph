using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public sealed class ReversedBidirectionalGraph<Vertex,Edge> : 
        IBidirectionalGraph<Vertex,ReversedEdge<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private readonly IBidirectionalGraph<Vertex,Edge> originalGraph;
        public ReversedBidirectionalGraph(IBidirectionalGraph<Vertex,Edge> originalGraph)
        {
            if (originalGraph==null)
                throw new ArgumentNullException("originalGraph");
            this.originalGraph = originalGraph;
        }

        public IBidirectionalGraph<Vertex,Edge> OriginalGraph
        {
            get { return this.originalGraph;}
        }
    
        public bool  IsVerticesEmpty
        {
        	get { return this.OriginalGraph.IsVerticesEmpty; }
        }

        public bool IsDirected
        {
            get { return this.OriginalGraph.IsDirected; }
        }

        public bool AllowParallelEdges
        {
            get { return this.OriginalGraph.AllowParallelEdges; }
        }

        public int  VertexCount
        {
        	get { return this.OriginalGraph.VertexCount; }
        }

        public IEnumerable<Vertex>  Vertices
        {
        	get { return this.OriginalGraph.Vertices; }
        }


        public bool ContainsVertex(Vertex vertex)
        {
            return this.OriginalGraph.ContainsVertex(vertex);
        }   

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            return this.OriginalGraph.ContainsEdge(target,source);
        }

        public bool  IsOutEdgesEmpty(Vertex v)
        {
            return this.OriginalGraph.IsInEdgesEmpty(v);
        }

        public int OutDegree(Vertex v)
        {
            return this.OriginalGraph.InDegree(v);
        }

        public IEnumerable<ReversedEdge<Vertex,Edge>> InEdges(Vertex v)
        {
            foreach(Edge edge in this.OriginalGraph.OutEdges(v))
                yield return new ReversedEdge<Vertex,Edge>(edge);
        }

        public ReversedEdge<Vertex,Edge> InEdge(Vertex v, int index)
        {
            Edge edge = this.OriginalGraph.OutEdge(v, index);
            if (edge == null)
                return null;
            return new ReversedEdge<Vertex, Edge>(edge);
        }

        public bool IsInEdgesEmpty(Vertex v)
        {
            return this.OriginalGraph.IsOutEdgesEmpty(v);
        }

        public int InDegree(Vertex v)
        {
            return this.OriginalGraph.OutDegree(v);
        }

        public IEnumerable<ReversedEdge<Vertex,Edge>>  OutEdges(Vertex v)
        {
            foreach(Edge edge in this.OriginalGraph.InEdges(v))
                yield return new ReversedEdge<Vertex,Edge>(edge);
        }

        public ReversedEdge<Vertex,Edge> OutEdge(Vertex v, int index)
        {
            Edge edge = this.OriginalGraph.InEdge(v, index);
            if (edge == null)
                return null;
            return new ReversedEdge<Vertex, Edge>(edge);
        }

        public IEnumerable<ReversedEdge<Vertex,Edge>>  Edges
        {
        	get 
            {
                foreach(Edge edge in this.OriginalGraph.Edges)
                    yield return new ReversedEdge<Vertex,Edge>(edge);
            }
        }

        public bool  ContainsEdge(ReversedEdge<Vertex,Edge> edge)
        {
            return this.OriginalGraph.ContainsEdge(edge.OriginalEdge);
        }

        public int Degree(Vertex v)
        {
            throw new NotImplementedException();
        }

        public bool IsEdgesEmpty
        {
            get { return this.OriginalGraph.IsEdgesEmpty; }
        }

        public int EdgeCount
        {
            get { return this.OriginalGraph.EdgeCount; }
        }
    }
}
