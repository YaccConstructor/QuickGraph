using System;

namespace QuickGraph.Petri
{
	/// <summary>
	/// A directed edge of a net which may connect a <see cref="IPlace&lt;Token&gt;"/>
	/// to a <see cref="ITransition&lt;Token&gt;"/> or a <see cref="ITransition&lt;Token&gt;"/> to
	/// a <see cref="IPlace&lt;Token&gt;"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Usually represented by an arrow.
	/// </para>
	/// </remarks>
	public interface IArc<Token>  
        : IEdge<IPetriVertex>
	{
		/// <summary>
		/// Gets a value indicating if the <see cref="IArc&lt;Token&gt;"/>
		/// instance is a <strong>input arc.</strong>
		/// </summary>
		/// <remarks>
		/// <para>
		/// An arc that leads from an input <see cref="IPlace&lt;Token&gt;"/> to a
		/// <see cref="ITransition&lt;Token&gt;"/> is called an <em>Input Arc</em> of
		/// the transition.
		/// </para>
		/// </remarks>
		bool IsInputArc {get;}

		/// <summary>
		/// Gets the <see cref="IPlace&lt;Token&gt;"/> instance attached to the
		/// <see cref="IArc&lt;Token&gt;"/>.
		/// </summary>
		/// <value>
		/// The <see cref="IPlace&lt;Token&gt;"/> attached to the <see cref="IArc&lt;Token&gt;"/>.
		/// </value>
		IPlace<Token> Place {get;}

		/// <summary>
		/// Gets or sets the <see cref="ITransition&lt;Token&gt;"/> instance attached to the
		/// <see cref="IArc&lt;Token&gt;"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ITransition&lt;Token&gt;"/> attached to the <see cref="IArc&lt;Token&gt;"/>.
		/// </value>
		ITransition<Token> Transition{get;}

		/// <summary>
		/// Gets or sets the arc annotation.
		/// </summary>
		/// <value>
		/// The <see cref="IExpression&lt;Token&gt;"/> annotation instance.
		/// </value>
		/// <remarks>
		/// <para>
		/// An expression that may involve constans, variables and operators
		/// used to annotate the arc. The expression evaluates over the type
		/// of the arc's associated place.
		/// </para>
        /// </remarks>
		/// <exception cref="ArgumentNullException">
		/// set property, value is a null reference (Nothing in Visual Basic).
		/// </exception>
		IExpression<Token> Annotation {get;set;}
	}
}
