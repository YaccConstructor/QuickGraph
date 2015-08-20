using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Collections
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class TermEdgeList<TVertex, TEdge>
        : IEdgeList<TVertex, TEdge>
#if !SILVERLIGHT
        , ICloneable
#endif
        where TEdge : ITermEdge<TVertex>
    {
        public TermEdgeList() 
        { }

        public TermEdgeList(int capacity)
        { }

        public TermEdgeList(TermEdgeList<TVertex, TEdge> list)
        {}

        public TermEdgeList<TVertex, TEdge> Clone()
        {
            return new TermEdgeList<TVertex, TEdge>(this);
        }

        IEdgeList<TVertex, TEdge> IEdgeList<TVertex,TEdge>.Clone()
        {
            return this.Clone();
        }

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif

        public void TrimExcess()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(TEdge item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TEdge item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TEdge this[int index]
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

        public void Add(TEdge item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TEdge item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TEdge[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TEdge item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TEdge> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
