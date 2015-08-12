using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A set of vertices
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    [ContractClass(typeof(IVertexSetContract<>))]
    public interface IVertexSet<TVertex>
        : IImplicitVertexSet<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether there are no vertices in this set.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the vertex set is empty; otherwise, <c>false</c>.
        /// </value>
        bool IsVerticesEmpty { get;}

        /// <summary>
        /// Gets the vertex count.
        /// </summary>
        /// <value>The vertex count.</value>
        int VertexCount { get;}

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        /// <value>The vertices.</value>
        IEnumerable<TVertex> Vertices { get;}
    }
}
