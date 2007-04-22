using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIndexedEnumerable<T> : IEnumerable<T>
    {
        int Count { get;}
        T this[int index] { get;}
        int IndexOf(T v);
    }
}
