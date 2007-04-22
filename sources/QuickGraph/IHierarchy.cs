using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IHierarchy<Vertex,Edge> : 
        IMutableVertexAndEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        /// <summary>
        /// Gets the roots of the hierarchy
        /// </summary>
        Vertex Root { get;}

        /// <summary>
        /// Gets the parent <typeparamref name="Vertex"/> of <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="vertex"/> is the root of the graph.
        /// </exception>
        Vertex GetParent(Vertex vertex);

        /// <summary>
        /// Gets the parent <typeparamref name="Edge"/> of <paramref name="vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="vertex"/> is the root of the graph.
        /// </exception>
        Edge GetParentEdge(Vertex vertex);

        /// <summary>
        /// Gets a value indicating if <paramref name="edge"/> is 
        /// a cross edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        bool IsCrossEdge(Edge edge);

        /// <summary>
        /// Gets a value indicating whether <paramref name="edge"/> 
        /// exists really or is just an induced edge.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        bool IsRealEdge(Edge edge);

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
        bool IsPredecessorOf(Vertex source, Vertex target);

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
        int InducedEdgeCount(Vertex source, Vertex target);

        /// <summary>
        /// Gets a value indicating if <paramref name="vertex"/> is 
        /// inner node or a leaf.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>
        /// true if not a leaf
        /// </returns>
        bool IsInnerNode(Vertex vertex);

        /// <summary>
        /// Gets the collection of children <typeparamref name="Edge"/>
        /// from <paramref name="Vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<Edge> ChildrenEdges(Vertex vertex);


        /// <summary>
        /// Gets the collection of children <typeparamref name="Vertex"/>
        /// from <paramref name="Vertex"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<Vertex> ChildrenVertices(Vertex vertex);
    }
}
