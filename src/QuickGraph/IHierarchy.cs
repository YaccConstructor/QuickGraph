using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IHierarchy<TVertex,TEdge> : 
        IMutableVertexAndEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets the roots of the hierarchy
        /// </summary>
        TVertex Root { get;}

        /// <summary>
        /// Gets the parent <typeparamref name="TVertex"/> of <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="vertex"/> is the root of the graph.
        /// </exception>
        TVertex GetParent(TVertex vertex);

        /// <summary>
        /// Gets the parent <typeparamref name="TEdge"/> of <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="vertex"/> is the root of the graph.
        /// </exception>
        TEdge GetParentEdge(TVertex vertex);

        /// <summary>
        /// Gets a value indicating if <paramref name="edge"/> is 
        /// a cross edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        bool IsCrossEdge(TEdge edge);

        /// <summary>
        /// Gets a value indicating whether <paramref name="edge"/> 
        /// exists really or is just an induced edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        bool IsRealEdge(TEdge edge);

        /// <summary>
        /// Gets a value indicating if <paramref name="source"/>
        /// is a predecessor of <paramref name="target"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>
        /// true if <paramref name="source"/> is a predecessor of
        /// <paramref name="target"/>
        /// </returns>
        bool IsPredecessorOf(TVertex source, TVertex target);

        /// <summary>
        /// Gets the number of edges between <paramref name="source"/>
        /// and <paramref name="target"/>. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="source"/> is a predecessor of <paramref name="target"/>
        /// or the otherway round.
        /// </exception>
        int InducedEdgeCount(TVertex source, TVertex target);

        /// <summary>
        /// Gets a value indicating if <paramref name="vertex"/> is 
        /// inner node or a leaf.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>
        /// true if not a leaf
        /// </returns>
        bool IsInnerNode(TVertex vertex);

        /// <summary>
        /// Gets the collection of children <typeparamref name="TEdge"/>
        /// from <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<TEdge> ChildrenEdges(TVertex vertex);


        /// <summary>
        /// Gets the collection of children <typeparamref name="TVertex"/>
        /// from <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<TVertex> ChildrenVertices(TVertex vertex);
    }
}
