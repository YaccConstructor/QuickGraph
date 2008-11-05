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
            CodeContract.Requires(elements != null);
            CodeContract.Requires(CodeContract.ForAll(elements, e => e != null));
        }
    }
}
