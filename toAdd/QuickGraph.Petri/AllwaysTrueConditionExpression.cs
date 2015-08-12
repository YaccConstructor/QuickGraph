using System;
using System.Collections.Generic;

namespace QuickGraph.Petri
{
    [Serializable]
    public sealed class AllwaysTrueConditionExpression<Token> : IConditionExpression<Token>
    {
		public bool IsEnabled(IList<Token> tokens)
		{
			return true;
		}
	}
}
