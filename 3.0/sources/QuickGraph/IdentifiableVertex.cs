using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An identifiable vertex.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{ID}")]
    public class IdentifiableVertex 
        : IIdentifiable
    {
        private readonly string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableVertex"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public IdentifiableVertex(string id)
        {
            Contract.Requires(id!=null);

            this.id = id;
        }

        /// <summary>
        /// Gets a string that uniquely indentifies the object.
        /// </summary>
        /// <value>The identity.</value>
        public string ID
        {
            [Pure]
            get { return this.id; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.id;
        }
    }
}
