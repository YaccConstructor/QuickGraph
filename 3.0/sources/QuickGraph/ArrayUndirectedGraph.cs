using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An immutable undirected graph data structure efficient for large sparse
    /// graph. NOT FINISHED
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    internal sealed class ArrayUndirectedGraph<TVertex, TEdge>
        : IUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly EdgeEqualityComparer<TVertex, TEdge> edgeEqualityComparer =
            EdgeExtensions.GetUndirectedVertexEquality<TVertex, TEdge>();
        readonly VertexIndexer<TVertex> vertexIndices;
        readonly int vertexCount;
        readonly int[] sourceEdgeStartIndices;
        readonly int[] targetEdgeStartIndices;
        readonly int[] targetEdgeFirstVertexOffsets;
        readonly TEdge[] edges;

        /// <summary>
        /// Initializes a new instance of the array adjacency graph.
        /// </summary>
        /// <param name="vertexCount">The number of vertices</param>
        /// <param name="edges">Array containing the edges. This array is stored and modified as is in the graph. No copy is made.</param>
        /// <param name="vertexIndices">A mapping from vertices to [0, vertexCount) range</param>
        public ArrayUndirectedGraph(            
            int vertexCount,
            TEdge[] edges,
            VertexIndexer<TVertex> vertexIndices)
        {
            Contract.Requires(vertexCount >= 0);
            Contract.Requires(edges != null);
            Contract.Requires(typeof(TEdge).IsValueType || Contract.ForAll(edges, e => e != null));
            Contract.Requires(vertexIndices != null);
#if CONTRACTS_BUG
            Contract.Requires(Contract.ForAll(edges, 
                e =>
                    0 <= vertexIndices(e.Source) && vertexIndices(e.Source) < vertexCount &&
                    0 <= vertexIndices(e.Target) && vertexIndices(e.Target) < vertexCount)
                    );
#endif

            this.vertexCount = vertexCount;
            this.vertexIndices = vertexIndices;
            this.edges = edges;

            // order out edges with respect to source, then target
            Array.Sort(this.edges, (l, r) =>
            {
                int c = vertexIndices(l.Source).CompareTo(vertexIndices(r.Source));
                if (c != 0) return c;
                return vertexIndices(l.Target).CompareTo(vertexIndices(r.Target));
            });

            // source-edge encoding, similar to ArrayAdjacencyGraph
            // first compute the number of out-edges per vertex
            this.sourceEdgeStartIndices = new int[vertexCount + 1];
            foreach (var edge in edges)
                this.sourceEdgeStartIndices[this.vertexIndices(edge.Source)]++;
            // patch with the edge start indices
            int counter = 0;
            for (int i = 0; i < this.sourceEdgeStartIndices.Length; i++)
            {
                int temp = counter;
                counter += this.sourceEdgeStartIndices[i];
                this.sourceEdgeStartIndices[i] = temp;
            }
            Contract.Assert(counter == edges.Length);
            this.sourceEdgeStartIndices[this.sourceEdgeStartIndices.Length - 1] = edges.Length;

            // target-edge encoding, much tricker
            this.targetEdgeStartIndices = new int[vertexCount];
            var targetOutIndices = new Dictionary<int, List<int>>();
            int indexCount = 0;
            for (int i = 0; i < this.edges.Length; i++)
            {
                var edge = this.edges[i];
                var targetIndex = vertexIndices(edge.Target);
                List<int> indices;
                if (!targetOutIndices.TryGetValue(targetIndex, out indices))
                    targetOutIndices[targetIndex] = indices = new List<int>(1);
                indices.Add(i);
                indexCount++;
            }
            this.targetEdgeFirstVertexOffsets = new int[indexCount];
            {
                int targetIndex = 0;
                int targetEdgeIndex = 0;
                foreach (var kv in targetOutIndices)
                {
                    kv.Value.CopyTo(this.targetEdgeFirstVertexOffsets, targetEdgeIndex);
                    this.targetEdgeStartIndices[targetIndex++] = targetEdgeIndex;
                    targetEdgeIndex += kv.Value.Count;
                }
                Contract.Assert(targetIndex == this.targetEdgeStartIndices.Length);
                Contract.Assert(targetEdgeIndex == this.targetEdgeFirstVertexOffsets.Length);
            }
        }


        public EdgeEqualityComparer<TVertex, TEdge> EdgeEqualityComparer
        {
            get
            {
                return this.edgeEqualityComparer;
            }
        }

        /// <summary>
        /// Gets the delegate used to map vertices to array indexes
        /// </summary>
        public VertexIndexer<TVertex> Indexer
        {
            get { return this.vertexIndices; }
        }

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
                for (int i = 0; i < this.sourceEdgeStartIndices.Length - 1; i++)
                {
                    int startIndex = this.sourceEdgeStartIndices[i];
                    int endIndex = this.sourceEdgeStartIndices[i + 1];
                    if (endIndex - startIndex > 0)
                        yield return this.edges[startIndex].Source;
                }
            }
        }
        #endregion

        #region IEdgeSet<TVertex,TEdge> Members
        public bool IsEdgesEmpty
        {
            get { return this.edges.Length == 0; }
        }

        public int EdgeCount
        {
            get { return this.edges.Length; }
        }

        public IEnumerable<TEdge> Edges
        {
            get { return this.edges; }
        }

        public bool ContainsEdge(TEdge edge)
        {
            var sourceIndex = this.vertexIndices(edge.Source);
            if (0 <= sourceIndex && sourceIndex < this.sourceEdgeStartIndices.Length - 1)
            {
                int startIndex = this.sourceEdgeStartIndices[sourceIndex];
                int endIndex = this.sourceEdgeStartIndices[sourceIndex + 1];
                for (int i = startIndex; i < endIndex; ++i)
                    if (this.edges.Equals(edge))
                        return true;
            }

            return false;
        }
        #endregion

        #region IUndirectedGraph<TVertex,TEdge> Members
        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            return null;
        }

        public int AdjacentDegree(TVertex v)
        {
            return 0;
        }

        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            foreach (var edge in this.AdjacentEdges(v))
                return false;
            return true;
        }

        public TEdge AdjacentEdge(TVertex v, int index)
        {
            throw new NotImplementedException();
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            throw new NotImplementedException();
        }

        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
