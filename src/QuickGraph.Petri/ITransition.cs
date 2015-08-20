using System;

namespace  QuickGraph.Petri
{
	/// <summary>
	/// A node of a net, taken from the transition kind.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Usually represented by a rectangle.
	/// </para>
	/// </remarks>
	public interface ITransition<Token> : IPetriVertex
	{
		/// <summary>
		/// A boolean expression associated with the transition
		/// </summary>
		IConditionExpression<Token> Condition {get;set;}
	}
}
