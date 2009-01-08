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
        public static T[] ToArray<T>(IEnumerable<T> values)
        {
            Contract.Requires(values != null);

            return new List<T>(values).ToArray();
        }

        [Pure]
        public static int Count<T>(IEnumerable<T> elements)
        {
            Contract.Requires(elements != null);

            ICollection<T> collection = elements as ICollection<T>;
            if (collection != null)
                return collection.Count;

            T[] array = elements as T[];
            if (array != null)
                return array.Length;

            int count = 0;
            foreach (var element in elements)
                count++;
            return count;
        }

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
        public static double Sum<T>(IEnumerable<T> elements, Func<T, double> map)
        {
            Contract.Requires(elements != null);
            Contract.Requires(map != null);
            double sum = 0;
            foreach (var element in elements)
                sum += map(element);
            return sum;
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