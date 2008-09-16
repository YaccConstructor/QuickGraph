using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    public static class Domains
    {
        public static EmptyDomain Empty
        {
            get
            {
                return new EmptyDomain();
            }
        }

        public static BooleanDomain Boolean
        {
            get { return new BooleanDomain(); }
        }

        public static bool IsUniform(IList<IDomain> domains)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");

            int maxCount = int.MinValue;
            int minCount = int.MaxValue;
            foreach (IDomain domain in domains)
            {
                maxCount = Math.Max(maxCount, domain.Count);
                minCount = Math.Max(minCount, domain.Count);
                if (maxCount != minCount)
                    return false;
            }
            return true;
        }

        public static IList<IDomain> Uniformize(params Object[] domains)
        {
            return Uniformize(ToDomains(domains));
        }

        public static IList<IDomain> Uniformize(params ICollection[] domains)
        {
            return Uniformize(ToDomains(domains));
        }

        public static IList<IDomain> Uniformize(params IEnumerable[] domains)
        {
            return Uniformize(ToDomains(domains));
        }

        public static IList<IDomain> Uniformize(IList<IDomain> domains)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");

            Random rnd = new Random((int)DateTime.Now.Ticks);
            // find max
            int maxCount = int.MinValue;
            int minCount = int.MaxValue;
            foreach (IDomain domain in domains)
            {
                maxCount = Math.Max(maxCount, domain.Count);
                minCount = Math.Max(minCount, domain.Count);
            }

            if (minCount == maxCount)
                return domains;

            IList<IDomain> udomains = new List<IDomain>();
            foreach (IDomain domain in domains)
            {
                if (domain.Count == maxCount)
                {
                    udomains.Add(domain);
                    continue;
                }

                Object[] udomain = new Object[maxCount];
                int i;
                for(i = 0;i<domain.Count;++i)
                    udomain[i] = domain[i];
                for (; i<maxCount;++i)
                {
                    udomain[i] = domain[ rnd.Next(domain.Count) ];
                }
                udomains.Add(Domains.ToDomain(udomain));
            }
            return udomains;
        }

        public static IList<IDomain> ToDomains(Array array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Length == 0)
                throw new ArgumentException("Length is zero", "array");
            IDomain[] domains = new IDomain[array.Length];
            for (int i = 0; i < array.Length; ++i)
                domains[i] = ToDomain(array.GetValue(i));
            return new List<IDomain>(domains);
        }

        public static IList<IDomain> ToDomains(params Object[] items)
        {
            if (items.Length == 0)
                throw new ArgumentException("Length is zero", "items");
            IList<IDomain> ds = new List<IDomain>();
            foreach (Object domain in items)
                ds.Add(ToDomain(domain));
            return ds;
        }

        public static IDomain ToDomain(Object item)
        {
            IDomain domain = item as IDomain;
            if (domain != null)
                return domain;

            String s = item as String;
            if (s != null)
                return ToDomain(s);

            Array array = item as Array;
            if (array != null)
                return ToDomain(array);

            ICollection collection = item as ICollection;
            if (collection != null)
                return ToDomain(collection);

            IEnumerable enumerable = item as IEnumerable;
            if (enumerable != null)
                return ToDomain(enumerable);

            return new CollectionDomain(new object[]{item});
        }

        public static IDomain ToDomain(string s)
        {
            return new StringDomain(s);
        }

        public static CollectionDomain ToDomain(ICollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is emtpy");
            return new CollectionDomain(collection);
        }

        public static CollectionDomain ToDomain(IEnumerable enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new CollectionDomain(enumerable);
        }

        public static ArrayDomain<T> ToDomain<T>(T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (items.Length == 0)
                throw new ArgumentException("No arguments");
            return new ArrayDomain<T>(items);
        }
    }
}
