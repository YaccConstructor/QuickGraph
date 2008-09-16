using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    public static class Products
    {
        #region Helpers
        private static void CheckDomains(IList<IDomain> domains)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");
            if (domains.Count == 0)
                throw new ArgumentException("domains is empty");
            for (int i = 0; i < domains.Count; ++i)
            {
                if (domains[i] == null)
                    throw new ArgumentNullException("Domain[" + i.ToString() + "] is null");
                if (domains[i].Count==0)
                    throw new ArgumentNullException("Domain[" + i.ToString() + "] is empty");
            }
        }
        #endregion

        #region Cartesian
        public static IEnumerable<ITuple> Cartesian(IList<IDomain> domains)
        {
            CheckDomains(domains);

            return new CartesianProductDomainTupleEnumerable(domains);
        }

        public static IEnumerable<ITuple> Cartesian(params IDomain[] domains)
        {
            return Cartesian(new List<IDomain>(domains));
        }

        public static IEnumerable<ITuple> Cartesian(params Object[] domains)
        {
            return Cartesian(Domains.ToDomains(domains));
        }
        #endregion

        #region PairWize
        public static IEnumerable<ITuple> PairWize(IList<IDomain> domains)
        {
            CheckDomains(domains);
            if (domains.Count <= 2)
                return Cartesian(domains);

            if (Domains.IsUniform(domains))
                return new UniformPairWizeProductDomainTupleEnumerable(domains);
            else
            {
                IList<IDomain> udomains = Domains.Uniformize(domains);
                return Greedy(new UniformPairWizeProductDomainTupleEnumerable(udomains));
            }
        }
        public static IEnumerable<ITuple> PairWize(params IDomain[] domains)
        {
            return PairWize(new List<IDomain>(domains));
        }
        public static IEnumerable<ITuple> PairWize(params Object[] domains)
        {
            return PairWize(Domains.ToDomains(domains));
        }
        #endregion

        #region TWize
        public static IEnumerable<ITuple> TWize(int tupleSize, IList<IDomain> domains)
        {
            CheckDomains(domains);

            IList<IDomain> udomains = Domains.Uniformize(domains);
            return new UniformTWizeProductDomainTupleEnumerable(udomains, tupleSize);
        }
        public static IEnumerable<ITuple> TWize(int tupleSize, params IDomain[] domains)
        {
            return TWize(tupleSize, new List<IDomain>(domains));
        }
        public static IEnumerable<ITuple> TWize(int tupleSize, params Object[] domains)
        {
            return TWize(tupleSize, Domains.ToDomains(domains));
        }
        #endregion

        public static IEnumerable<ITuple> Greedy(IEnumerable<ITuple> tuples)
        {
            if (tuples == null)
                throw new ArgumentNullException("tuples");
            return new GreedyTupleEnumerable(tuples);
        }


        public static IEnumerable<ITuple> ComputeTupleProducts(
            IList<IDomain> domains,
            CombinationType combinationType)
        {
            switch (combinationType)
            {
                case CombinationType.PairWize:
                    return Products.PairWize(domains);
                case CombinationType.Cartesian:
                    return Products.Cartesian(domains);
                default:
                    throw new NotSupportedException(combinationType.ToString());
            }
        }
    }
}
