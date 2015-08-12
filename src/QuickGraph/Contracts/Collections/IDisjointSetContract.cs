using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections.Contracts
{
    [ContractClassFor(typeof(IDisjointSet<>))]
    abstract class IDisjointSetContract<T>
        : IDisjointSet<T>
    {
        int IDisjointSet<T>.SetCount
        {
            get
            {
                return default(int);
            }
        }

        int IDisjointSet<T>.ElementCount
        {
            get
            {
                return default(int);
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            IDisjointSet<T> ithis = this;
            Contract.Invariant(0 <= ithis.SetCount);
            Contract.Invariant(ithis.SetCount <= ithis.ElementCount);
        }


        void IDisjointSet<T>.MakeSet(T value)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(value != null);
            Contract.Requires(!ithis.Contains(value));
            Contract.Ensures(ithis.Contains(value));
            Contract.Ensures(ithis.SetCount == Contract.OldValue(ithis.SetCount) + 1);
            Contract.Ensures(ithis.ElementCount == Contract.OldValue(ithis.ElementCount) + 1);
        }

        T IDisjointSet<T>.FindSet(T value)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(value != null);
            Contract.Requires(ithis.Contains(value));

            return default(T);
        }

        bool IDisjointSet<T>.Union(T left, T right)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(left != null);
            Contract.Requires(ithis.Contains(left));
            Contract.Requires(right != null);
            Contract.Requires(ithis.Contains(right));

            return default(bool);
        }

        [Pure]
        bool IDisjointSet<T>.Contains(T value)
        {
            return default(bool);
        }

        bool IDisjointSet<T>.AreInSameSet(T left, T right)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(ithis.Contains(left));
            Contract.Requires(ithis.Contains(right));

            return default(bool);
        }
    }
}
