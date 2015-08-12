using System;

namespace QuickGraph.Petri
{
	/// <summary>
	/// A vertex (node) of a Petri Graph.
	/// </summary>
    public interface IPetriVertex
    {
		/// <summary>
		/// Gets or sets the name of the node
		/// </summary>
		/// <value>
		/// A <see cref="String"/> representing the name of the node.
		/// </value>
		String Name {get;}
    }
}
