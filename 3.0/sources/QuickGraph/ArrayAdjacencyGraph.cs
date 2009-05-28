using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An immutable directed graph data structure efficient for large sparse
    /// graph representation where out-edge need to be enumerated only.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public sealed class ArrayAdjacencyGraph<TVertex, TEdge>
        : IVertexAndEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly VertexIndexer<TVertex> vertexIndices;
        readonly int vertexCount;
        readonly int[] edgeStartIndices;
        readonly TEdge[] outEdges;

        /// <summary>
        /// Initializes a new instance of the array adjacency graph.
        /// </summary>
        /// <param name="vertexCount">The number of vertices</param>
        /// <param name="edges">Array containing the edges. This array is stored and modified as is in the graph. No copy is made.</param>
        /// <param name="vertexIndices">A mapping from vertices to [0, vertexCount) range</param>
        public ArrayAdjacencyGraph(            
            int vertexCount,
            TEdge[] edges,
            VertexIndexer<TVertex> vertexIndices)
        {
            Contract.Requires(vertexCount >= 0);
            Contract.Requires(edges != null);
            Contract.Requires(typeof(TEdge).IsValueType || Contract.ForAll(edges, e => e != null));
            Contract.Requires(vertexIndices != null);
            Contract.Requires(Contract.ForAll(edges, 
                e =>
                    0 <= vertexIndices(e.Source) && 
                    vertexIndices(e.Source) < vertexCount &&
                    0 <= vertexIndices(e.Target) && 
                    vertexIndices(e.Target) < vertexCount)
                    );

            this.vertexCount = vertexCount;
            this.vertexIndices = vertexIndices;
            this.edgeStartIndices = new int[vertexCount + 1];
            this.outEdges = edges;

            // first compute the number of out-edges per vertex
            foreach (var edge in edges)
                this.edgeStartIndices[this.vertexIndices(edge.Source)]++;            
            // patch with the edge start indices
            int counter = 0;
            for (int i = 0; i < this.edgeStartIndices.Length; i++)
            {
                int temp = counter;
                counter += this.edgeStartIndices[i];
                this.edgeStartIndices[i] = temp;
            }
            Contract.Assert(counter == edges.Length);
            this.edgeStartIndices[this.edgeStartIndices.Length - 1] = edges.Length;

            // order out edges with respect to source
            Array.Sort(this.outEdges, (l, r) => vertexIndices(l.Source).CompareTo(vertexIndices(l.Target)));
        }

        /// <summary>
        /// Gets the delegate used to map vertices to array indexes
        /// </summary>
        public VertexIndexer<TVertex> Indexer
        {
            get { return this.vertexIndices; }
        }

        #region IIncidenceGraph<TVertex,TEdge> Members
        public bool ContainsEdge(TVertex source, TVertex target)
        {
            TEdge edge;
            return this.TryGetEdge(source, target, out edge);
        }

        public bool TryGetEdges(TVertex source, TVertex target, out IEnumerable<TEdge> edges)
        {
            int sourceIndex = this.vertexIndices(source);
            if (0 <= sourceIndex && sourceIndex < this.vertexCount)
            {
                int targetIndex = this.vertexIndices(target);
                if (0 <= targetIndex && targetIndex < this.vertexCount)
                {
                    int endIndex = this.edgeStartIndices[sourceIndex + 1];

                    edges = this.GetEdges(sourceIndex, endIndex - sourceIndex);
                    return true;
                }
            }

            edges = null;
            return false;
        }

        private IEnumerable<TEdge> GetEdges(int startIndex, int endIndex)
        {
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= endIndex);
            Contract.Requires(endIndex < this.outEdges.Length);

            for (int i = startIndex; i < endIndex; ++i)
                yield return this.outEdges[i];
        }

        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            int sourceIndex = this.vertexIndices(source);
            if (0 <= sourceIndex && sourceIndex < this.vertexCount)
            {
                int targetIndex = this.vertexIndices(target);
                if (0 <= targetIndex && targetIndex < this.vertexCount)
                {
                    int endIndex = this.edgeStartIndices[sourceIndex + 1];
                    for (int i = sourceIndex; i < endIndex; ++i)
                    {
                        var e = this.outEdges[this.edgeStartIndices[i]];
                        if (e.Target.Equals(target))
                        {
                            edge = e;
                            return true;
                        }
                    }
                }
            }

            edge = default(TEdge);
            return false;
        }

        #endregion

        #region IImplicitGraph<TVertex,TEdge> Members
        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(TVertex v)
        {
            int index = this.vertexIndices(v);
            return this.edgeStartIndices[index + 1] - this.edgeStartIndices[index];
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            int index = this.vertexIndices(v);
            return this.GetEdges(this.edgeStartIndices[index], this.edgeStartIndices[index + 1]);
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            int sourceIndex = this.vertexIndices(v);
            if (0 <= sourceIndex && sourceIndex < this.vertexCount)
            {
                int start = this.edgeStartIndices[sourceIndex];
                int end = this.edgeStartIndices[sourceIndex + 1];
                if (end - start > 0)
                {
                    edges = this.GetEdges(start, end);
                    return true;
                }
            }

            edges = null;
            return false;
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            int vindex = this.vertexIndices(v);
            return this.outEdges[this.edgeStartIndices[vindex] + index];
        }
        #endregion

        #region IGraph<TVertex,TEdge> Members\
        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return true; }
        }        
        #endregion

        #region IImplicitVertexSet<TVertex> Members
        public bool ContainsVertex(TVertex vertex)
        {
            var index = this.vertexIndices(vertex);
            return 0 <= index && index < this.vertexCount;
        }
        #endregion

        #region IVertexSet<TVertex> Members
        public bool IsVerticesEmpty
        {
            get { return this.vertexCount == 0; }
        }

        public int VertexCount
        {
            get { return this.vertexCount; }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                for (int i = 0; i < this.edgeStartIndices.Length - 1; i++)
                {
                    int startIndex = this.edgeStartIndices[i];
                    int endIndex = this.edgeStartIndices[i + 1];
                    if (endIndex - startIndex > 0)
                        yield return this.outEdges[startIndex].Source;
                }
            }
        }
        #endregion

        #region IEdgeSet<TVertex,TEdge> Members
        public bool IsEdgesEmpty
        {
            get { return this.outEdges.Length == 0; }
        }

        public int EdgeCount
        {
            get { return this.outEdges.Length; }
        }

        public IEnumerable<TEdge> Edges
        {
            get { return this.outEdges; }
        }

        public bool ContainsEdge(TEdge edge)
        {
            var sourceIndex = this.vertexIndices(edge.Source);
            if (0 <= sourceIndex && sourceIndex < this.edgeStartIndices.Length - 1)
            {
                int startIndex = this.edgeStartIndices[sourceIndex];
                int endIndex = this.edgeStartIndices[sourceIndex + 1];
                for (int i = startIndex; i < endIndex; ++i)
                    if (this.outEdges.Equals(edge))
                        return true;
            }

            return false;
        }
        #endregion
    }
}
