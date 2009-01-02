using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An identifiable object
    /// </summary>
#if CONTRACTS_FULL
    [ContractClass(typeof(IIdentifiableContract))]
#endif
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets a string that uniquely indentifies the object.
        /// </summary>
        /// <value>The identity.</value>
        string ID { get;}
    }
}
