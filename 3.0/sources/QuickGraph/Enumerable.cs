#if NET20
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System.Linq
{
    public static class Enumerable
    {
        public static T First<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);
            foreach (var element in elements)
                return element;
            throw new ArgumentException();
        }

        public static T FirstOrDefault<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);
            foreach (var element in elements)
                return element;
            return default(T);
        }
    }
}
#endif