using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    public static class EnumerableContract
    {
        [Pure, Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void RequiresElementsNotNull<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, e => e != null));
        }
    }
}
