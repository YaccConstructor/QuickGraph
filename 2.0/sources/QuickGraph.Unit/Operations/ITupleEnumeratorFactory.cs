using System;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
	public interface ITupleEnumeratorFactory
	{
		IEnumerator<ITuple> Create(ITuple tuple);
	}
}
