using System;
using System.Collections;

namespace QuickGraph.Operations
{
	public interface ITuple : IEnumerable, IComparable
	{
        int Count { get;}
        Object this[int i] { get;}
        void Add(Object item);
        void Concat(ITuple tuple);

        Object[] ToObjectArray();
    }
}
