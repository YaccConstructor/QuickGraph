using System;
using System.Collections.Generic;

namespace  QuickGraph.Petri
{
	public interface IConditionExpression<Token>
	{
		bool IsEnabled(IList<Token> tokens);
	}
}
