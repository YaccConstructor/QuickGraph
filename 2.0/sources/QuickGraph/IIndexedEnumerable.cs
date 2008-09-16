using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface ICountable<T> : IEnumerable<T>
    {
        int Count { get;}
    }

    public interface IIndexable<T> : ICountable<T>
    {
        T this[int index] { get;}
        int IndexOf(T v);
    }
}
