using System;
using System.Collections.Generic;

namespace QuickGraph.Petri
{
	/// <summary>
	/// A Place in the HLPN framework
	/// </summary>
	/// <remarks>
	/// <para>
	/// A <see cref="Place"/> is characterized by a set of tokens, called the
	/// <see cref="Marking"/> of the place. The place is <strong>typed</strong>
	/// by the <see cref="StrongType"/> instance. This means only object
	/// of <see cref="Type"/> assignable to <see cref="StrongType"/> can reside
	/// in the place.
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
