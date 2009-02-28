using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [ContractClass(typeof(IMutableVertexAndEdgeSetContract<,>))]
    public interface IMutableVertexAndEdgeSet<TVertex,TEdge>
        : IEdgeListGraph<TVertex, TEdge>
        , IMutableVertexSet<TVertex>
        , IMutableEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Adds the vertices and edge to the graph.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns>true if the edge was added, otherwise false.</returns>
        bool AddVerticesAndEdge(TEdge edge);

        /// <summary>
        /// Adds a set of edges (and it's vertices if necessary)
        /// </summary>
        /// <param name="edges"></param>
        /// <returns>the number of edges added.</returns>
        int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges);
    }
}
