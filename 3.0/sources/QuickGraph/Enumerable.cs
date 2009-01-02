#if NET20
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph;

namespace System.Linq
{
    public static class Enumerable
    {
        [Pure]
        public static T First<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);
            foreach (var element in elements)
                return element;
            throw new ArgumentException();
        }

        [Pure]
        public static T FirstOrDefault<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);
            foreach (var element in elements)
                return element;
            return default(T);
        }

        [Pure]
        public static int Sum<T>(IEnumerable<T> elements, Func<T, int> map)
        {
            Contract.Requires(elements != null);
            Contract.Requires(map != null);
            int sum = 0;
            foreach (var element in elements)
                sum += map(element);
            return sum;
        }
    }
}
#endif