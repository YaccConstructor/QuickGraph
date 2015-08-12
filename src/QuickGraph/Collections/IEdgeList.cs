using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    /// <summary>
    /// A cloneable list of edges
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    [ContractClass(typeof(IEdgeListContract<,>))]
    public interface IEdgeList<TVertex, TEdge>
        : IList<TEdge>
        #if !SILVERLIGHT
        , ICloneable
        #endif
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Trims excess allocated space
        /// </summary>
        void TrimExcess();
        /// <summary>
        /// Gets a clone of this list
        /// </summary>
        /// <returns></returns>
#if !SILVERLIGHT
        new 
#endif
        IEdgeList<TVertex, TEdge> Clone();
    }
}
