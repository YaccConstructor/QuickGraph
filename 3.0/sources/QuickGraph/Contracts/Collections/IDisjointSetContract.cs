#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections.Contracts
{
    [ContractClassFor(typeof(IDisjointSet<>))]
    class IDisjointSetContract<T>
        : IDisjointSet<T>
    {
        int IDisjointSet<T>.SetCount
        {
            get
            {
                return Contract.Result<int>();
            }
        }

        int IDisjointSet<T>.ElementCount
        {
            get
            {
                return Contract.Result<int>();
            }
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
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

        object IDisjointSet<T>.FindSet(T value)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(value != null);
            Contract.Requires(ithis.Contains(value));
            Contract.Ensures(Contract.Result<object>() != null);

            return Contract.Result<object>();
        }

        void IDisjointSet<T>.Union(T left, T right)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(left != null);
            Contract.Requires(ithis.Contains(left));
            Contract.Requires(right != null);
            Contract.Requires(ithis.Contains(right));
        }

        [Pure]
        bool IDisjointSet<T>.Contains(T value)
        {
            return Contract.Result<bool>();
        }

        bool IDisjointSet<T>.AreInSameSet(T left, T right)
        {
            IDisjointSet<T> ithis = this;
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(ithis.Contains(left));
            Contract.Requires(ithis.Contains(right));

            return Contract.Result<bool>();
        }
    }
}
#endif