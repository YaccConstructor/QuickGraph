using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    /// <summary>
    /// A disjoint-set data structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if CONTRACTS_FULL
    [ContractClass(typeof(QuickGraph.Collections.Contracts.IDisjointSetContract<>))]
#endif
    public interface IDisjointSet<T>
    {
        /// <summary>
        /// Gets the current number of sets
        /// </summary>
        int SetCount { get; }

        /// <summary>
        /// Gets the current number of elements.
        /// </summary>
        int ElementCount { get; }

        /// <summary>
        /// Creates a new set for the value
        /// </summary>
        /// <param name="value"></param>
        void MakeSet(T value);

        /// <summary>
        /// Finds the set containing the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object FindSet(T value);

        /// <summary>
        /// Gets a value indicating if left and right are contained in the same set
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        bool AreInSameSet(T left, T right);

        /// <summary>
        /// Merges the sets from the two values
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        void Union(T left, T right);

        /// <summary>
        /// Gets a value indicating whether the value is in the data structure
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Contains(T value);
    }
}
