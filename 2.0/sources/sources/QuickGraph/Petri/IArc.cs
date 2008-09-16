using System;

namespace QuickGraph.Petri
{
	/// <summary>
	/// A directed edge of a net which may connect a <see cref="IPlace"/>
	/// to a <see cref="ITransition"/> or a <see cref="ITransition"/> to
	/// a <see cref="IPlace"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Usually represented by an arrow.
	/// </para>
	/// </remarks>
	public interface IArc<Token>  : IEdge<IPetriVertex>
	{
		/// <summary>
		/// Gets or sets a value indicating if the <see cref="IArc"/>
		/// instance is a <strong>input arc.</strong>
		/// </summary>
		/// <remarks>
		/// <para>
		/// An arc that leads from an input <see cref="IPlace"/> to a
		/// <see cref="ITransition"/> is called an <em>Input Arc</em> of
		/// the transition.
		/// </para>
		/// </remarks>
		bool IsInputArc {get;}

		/// <summary>
		/// Gets or sets the <see cref="IPlace"/> instance attached to the
		/// <see cref="IArc"/>.
		/// </summary>
		/// <value>
		/// The <see cref="IPlace"/> attached to the <see cref="IArc"/>.
		/// </value>
		/// <exception cref="ArgumentNullException">
		/// set property, value is a null reference (Nothing in Visual Basic).
		/// </exception>
		IPlace<Token> Place {get;}

		/// <summary>
		/// Gets or sets the <see cref="ITransition"/> instance attached to the
		/// <see cref="IArc"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ITransition"/> attached to the <see cref="IArc"/>.
		/// </value>
		/// <exception cref="ArgumentNullException">
		/// set property, value is a null reference (Nothing in Visual Basic).
		/// </exception>
		ITransition<Token> Transition{get;}

		/// <summary>
		/// Gets or sets the arc annotation.
		/// </summary>
		/// <value>
		/// The <see cref="IExpression"/> annotation instance.
		/// </value>
		/// <remarks>
		/// <para>
		/// An expression that may involve constans, variables and operators
		/// used to annotate the arc. The expression evaluates over the type
		/// of the arc's associated place.
		/// </para>
		/// </summary>
		/// <exception cref="ArgumentNullException">
		/// set property, value is a null reference (Nothing in Visual Basic).
		/// </exception>
		IExpression<Token> Annotation {get;set;}
	}
}
