using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public sealed class ReversedBidirectionalGraph<TVertex, TEdge> : 
        IBidirectionalGraph<TVertex,ReversedEdge<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IBidirectionalGraph<TVertex,TEdge> originalGraph;
        public ReversedBidirectionalGraph(IBidirectionalGraph<TVertex,TEdge> originalGraph)
        {
            Contract.Requires(originalGraph != null);
            this.originalGraph = originalGraph;
        }

        public IBidirectionalGraph<TVertex,TEdge> OriginalGraph
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

        public IEnumerable<TVertex> Vertices
        {
        	get { return this.OriginalGraph.Vertices; }
        }

        [Pure]
        public bool ContainsVertex(TVertex vertex)
        {
            return this.OriginalGraph.ContainsVertex(vertex);
        }   

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            return this.OriginalGraph.ContainsEdge(target,source);
        }

        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out ReversedEdge<TVertex, TEdge> edge)
        {
            TEdge oedge;
            if (this.OriginalGraph.TryGetEdge(target, source, out oedge))
            {
                edge = new ReversedEdge<TVertex, TEdge>(oedge);
                return true;
            }
            else
            {
                edge = default(ReversedEdge<TVertex, TEdge>);
                return false;
            }
        }

        public bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<ReversedEdge<TVertex,TEdge>> edges)
        {
            IEnumerable<TEdge> oedges;
            if (this.OriginalGraph.TryGetEdges(target, source, out oedges))
            {
                List<ReversedEdge<TVertex, TEdge>> list = new List<ReversedEdge<TVertex, TEdge>>();
                foreach (var oedge in oedges)
                    list.Add(new ReversedEdge<TVertex, TEdge>(oedge));
                edges = list;
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        [Pure]
        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.OriginalGraph.IsInEdgesEmpty(v);
        }

        [Pure]
        public int OutDegree(TVertex v)
        {
            return this.OriginalGraph.InDegree(v);
        }

        [Pure]
        public IEnumerable<ReversedEdge<TVertex, TEdge>> InEdges(TVertex v)
        {
            foreach(TEdge edge in this.OriginalGraph.OutEdges(v))
                yield return new ReversedEdge<TVertex,TEdge>(edge);
        }

        [Pure]
        public ReversedEdge<TVertex, TEdge> InEdge(TVertex v, int index)
        {
            TEdge edge = this.OriginalGraph.OutEdge(v, index);
            if (edge == null)
                return default(ReversedEdge<TVertex,TEdge>);
            return new ReversedEdge<TVertex, TEdge>(edge);
        }

        [Pure]
        public bool IsInEdgesEmpty(TVertex v)
        {
            return this.OriginalGraph.IsOutEdgesEmpty(v);
        }

        [Pure]
        public int InDegree(TVertex v)
        {
            return this.OriginalGraph.OutDegree(v);
        }

        [Pure]
        public IEnumerable<ReversedEdge<TVertex, TEdge>> OutEdges(TVertex v)
        {
            foreach(TEdge edge in this.OriginalGraph.InEdges(v))
                yield return new ReversedEdge<TVertex,TEdge>(edge);
        }

        [Pure]
        public bool TryGetInEdges(TVertex v, out IEnumerable<ReversedEdge<TVertex, TEdge>> edges)
        {
            if (this.ContainsVertex(v))
            {
                edges = this.InEdges(v);
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        [Pure]
        public bool TryGetOutEdges(TVertex v, out IEnumerable<ReversedEdge<TVertex, TEdge>> edges)
        {
            if (this.ContainsVertex(v))
            {
                edges = this.OutEdges(v);
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        [Pure]
        public ReversedEdge<TVertex, TEdge> OutEdge(TVertex v, int index)
        {
            TEdge edge = this.OriginalGraph.InEdge(v, index);
            if (edge == null)
                return default(ReversedEdge<TVertex,TEdge>);
            return new ReversedEdge<TVertex, TEdge>(edge);
        }

        public IEnumerable<ReversedEdge<TVertex,TEdge>>  Edges
        {
        	get 
            {
                foreach(TEdge edge in this.OriginalGraph.Edges)
                    yield return new ReversedEdge<TVertex,TEdge>(edge);
            }
        }

        [Pure]
        public bool ContainsEdge(ReversedEdge<TVertex, TEdge> edge)
        {
            return this.OriginalGraph.ContainsEdge(edge.OriginalEdge);
        }

        [Pure]
        public int Degree(TVertex v)
        {
            return this.OriginalGraph.Degree(v);
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
