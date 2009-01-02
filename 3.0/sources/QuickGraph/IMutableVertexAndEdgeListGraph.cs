using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableVertexAndEdgeListGraph<TVertex,TEdge> :
        IMutableVertexListGraph<TVertex,TEdge>,
        IMutableEdgeListGraph<TVertex,TEdge>,
        IVertexAndEdgeListGraph<TVertex,TEdge>
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
