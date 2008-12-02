using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    /// <summary>
    /// An identifiable object
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets a string that uniquely indentifies the object.
        /// </summary>
        /// <value>The identity.</value>
        string ID { get;}
    }
}
