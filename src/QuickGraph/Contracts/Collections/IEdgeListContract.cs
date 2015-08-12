using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    [ContractClassFor(typeof(IEdgeList<,>))]
    abstract class IEdgeListContract<TVertex,TEdge> 
        : IEdgeList<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        IEdgeList<TVertex, TEdge> IEdgeList<TVertex, TEdge>.Clone()
        {
            Contract.Ensures(Contract.Result<IEdgeList<TVertex, TEdge>>() != null);
            throw new NotImplementedException();
        }

        void IEdgeList<TVertex, TEdge>.TrimExcess()
        { }

        #region others
        int IList<TEdge>.IndexOf(TEdge item)
        {
            throw new NotImplementedException();
        }

        void IList<TEdge>.Insert(int index, TEdge item)
        {
            throw new NotImplementedException();
        }

        void IList<TEdge>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        TEdge IList<TEdge>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void ICollection<TEdge>.Add(TEdge item)
        {
            throw new NotImplementedException();
        }

        void ICollection<TEdge>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<TEdge>.Contains(TEdge item)
        {
            throw new NotImplementedException();
        }

        void ICollection<TEdge>.CopyTo(TEdge[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<TEdge>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<TEdge>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<TEdge>.Remove(TEdge item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<TEdge> IEnumerable<TEdge>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

#if!SILVERLIGHT
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
#endif
        #endregion
    }
}
