using System;
using System.Collections;

namespace QuickGraph.Operations
{
	public interface IDomain : IEnumerable
	{
        string Name { get;set;}
        int Count { get;}
        Object this[int i] {get;}
    }
}
