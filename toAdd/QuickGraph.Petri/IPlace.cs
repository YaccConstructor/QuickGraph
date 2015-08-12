using System;
using System.Collections.Generic;

namespace QuickGraph.Petri
{
	/// <summary>
	/// A Place in the HLPN framework
	/// </summary>
	/// <remarks>
	/// <para>
	/// A <see cref="Place&lt;Token&gt;"/> is characterized by a set of tokens, called the
	/// <see cref="Marking"/> of the place.
	/// </para>
	/// <para>
	/// Usually represented by an ellipses (often circles).
	/// </para>
	/// </remarks>
    public interface IPlace<Token> : IPetriVertex
    {
		IList<Token> Marking {get;}

        string ToStringWithMarking();
    }
}
