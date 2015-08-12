using System;
using System.Collections.Generic;

namespace  QuickGraph.Petri
{
	public interface IExpression<Token>
	{
		IList<Token> Eval(IList<Token> marking);
	}
}
