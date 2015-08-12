using System;
using System.Collections.Generic;

namespace  QuickGraph.Petri
{
	/// <summary>
	/// A High Level Petri Graph.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This object is called a Petri Net in honour of Petri's work. In fact,
	/// it should be named High Level Petri Graph.
	/// </para>
	/// </remarks>
	public interface IPetriNet<Token>
    {
		/// <summary>
		/// Gets a collection of <see cref="IPlace&lt;Token&gt;"/> instances.
		/// </summary>
		/// <value>
		/// A collection of <see cref="IPlace&lt;Token&gt;"/> instances.
		/// </value>
		IList<IPlace<Token>> Places {get;}

		/// <summary>
		/// Gets a collection of <see cref="ITransition&lt;Token&gt;"/> instances.
		/// </summary>
		/// <value>
		/// A collection of <see cref="ITransition&lt;Token&gt;"/> instances.
		/// </value>
        IList<ITransition<Token>> Transitions { get;}

        /// <summary>
		/// Gets a collection of <see cref="IArc&lt;Token&gt;"/> instances.
		/// </summary>
		/// <value>
		/// A collection of <see cref="IArc&lt;Token&gt;"/> instances.
		/// </value>
        IList<IArc<Token>> Arcs { get;}

        IPetriGraph<Token> Graph { get;}
    }
}
