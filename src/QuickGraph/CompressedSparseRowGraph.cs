using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// Directed graph representation using a Compressed Sparse Row representation
    /// (http://www.cs.utk.edu/~dongarra/etemplates/node373.html)
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public sealed class CompressedSparseRowGraph<TVertex>
        : IVertexSet<TVertex>
        , IEdgeSet<TVertex, SEquatableEdge<TVertex>>
        , IVertexListGraph<TVertex, SEquatableEdge<TVertex>>
#if !SILVERLIGHT
        , ICloneable
#endif
    {
#if !SILVERLIGHT
        [Serializable]
#endif
        struct Range
        {
            public readonly int Start;
            public readonly int End;
            public Range(int start, int end)
            {
                Contract.Requires(start >= 0);
                Contract.Requires(start <= end);
                Contract.Ensures(Contract.ValueAtReturn(out this).Start == start);
                Contract.Ensures(Contract.ValueAtReturn(out this).End == end);

                this.Start = start;
                this.End = end;
            }

            public int Length
            {
                get {
                    Contract.Ensures(Contract.Result<int>() >= 0);

                    return this.End - this.Start;
                }
            }
        }

        readonly Dictionary<TVertex, Range> outEdgeStartRanges;
        readonly TVertex[] outEdges;

        private CompressedSparseRowGraph(
            Dictionary<TVertex, Range> outEdgeStartRanges,
            TVertex[] outEdges
            )
        {
            Contract.Requires(outEdgeStartRanges != null);
            Contract.Requires(outEdges != null);

            this.outEdgeStartRanges = outEdgeStartRanges;
            this.outEdges = outEdges;
        }

        public static CompressedSparseRowGraph<TVertex> FromGraph<TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Ensures(Contract.Result<CompressedSparseRowGraph<TVertex>>() != null);

            var outEdgeStartRanges = new Dictionary<TVertex, Range>(visitedGraph.VertexCount);
            var outEdges = new TVertex[visitedGraph.EdgeCount];

            int start = 0;
            int end = 0;
            int index = 0;
            foreach (var vertex in visitedGraph.Vertices)
            {
                end = index + visitedGraph.OutDegree(vertex);
                var range = new Range(start, end);
                outEdgeStartRanges.Add(vertex, range);
                foreach (var edge in visitedGraph.OutEdges(vertex))
                    outEdges[index++] = edge.Target;
                Contract.Assert(index == end);
            }
            Contract.Assert(index == outEdges.Length);

            return new CompressedSparseRowGraph<TVertex>(
                outEdgeStartRanges,
                outEdges);
        }

        public bool IsVerticesEmpty
        {
            get { return this.outEdgeStartRanges.Count > 0; }
        }

        public int VertexCount
        {
            get { return this.outEdgeStartRanges.Count; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return this.outEdgeStartRanges.Keys; }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            return this.outEdgeStartRanges.ContainsKey(vertex);
        }

        public int EdgeCount
        {
            get { return this.outEdges.Length; }
        }

        public bool IsEdgesEmpty
        {
            get { return this.outEdges.Length > 0; }
        }

        public IEnumerable<SEquatableEdge<TVertex>> Edges
        {
            get 
            {
                foreach (var kv in this.outEdgeStartRanges)
                {
                    var source = kv.Key;
                    var range = kv.Value;
                    for (int i = range.Start; i < range.End; ++i)
                    {
                        var target = this.outEdges[i];
                        yield return new SEquatableEdge<TVertex>(source, target);
                    }
                }
            }
        }

        public bool ContainsEdge(SEquatableEdge<TVertex> edge)
        {
            return ContainsEdge(edge.Source, edge.Target);
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            Range range;
            if (this.outEdgeStartRanges.TryGetValue(source, out range))
            {
                for (int i = range.Start; i < range.End; ++i)
                    if (this.outEdges[i].Equals(target))
                        return true;
            }

            return false;
        }

        public bool TryGetEdges(
            TVertex source, 
            TVertex target, 
            out IEnumerable<SEquatableEdge<TVertex>> edges)
        {
            if (this.ContainsEdge(source, target))
            {
                edges = new SEquatableEdge<TVertex>[] { new SEquatableEdge<TVertex>(source, target) };
                return true;
            }

            edges = null;
            return false;
        }

        public bool TryGetEdge(
            TVertex source, 
            TVertex target, 
            out SEquatableEdge<TVertex> edge)
        {
            if (this.ContainsEdge(source, target))
            {
                edge = new SEquatableEdge<TVertex>(source, target);
                return true;
            }

            edge = default(SEquatableEdge<TVertex>);
            return false;
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.outEdgeStartRanges[v].Length == 0;
        }

        public int OutDegree(TVertex v)
        {
            return this.outEdgeStartRanges[v].Length;
        }

        public IEnumerable<SEquatableEdge<TVertex>> OutEdges(TVertex v)
        {
            var range = this.outEdgeStartRanges[v];
            for(int i = range.Start;i<range.End;++i)
                yield return new SEquatableEdge<TVertex>(v, this.outEdges[i]);
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<SEquatableEdge<TVertex>> edges)
        {
            Range range;
            if (this.outEdgeStartRanges.TryGetValue(v, out range) &&
                range.Length > 0)
            {
                edges = this.OutEdges(v);
                return true;
            }

            edges = null;
            return false;
        }

        public SEquatableEdge<TVertex> OutEdge(TVertex v, int index)
        {
            var range = this.outEdgeStartRanges[v];
            var targetIndex = range.Start + index;
            Contract.Assert(targetIndex < range.End);
            return new SEquatableEdge<TVertex>(v, this.outEdges[targetIndex]);
        }

        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return false; }
        }

#if !SILVERLIGHT
        public CompressedSparseRowGraph<TVertex> Clone()
        {
            var ranges = new Dictionary<TVertex, Range>(this.outEdgeStartRanges);
            var edges = (TVertex[])this.outEdges.Clone();
            return new CompressedSparseRowGraph<TVertex>(ranges, edges);
        }
#endif

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif
    }
}
