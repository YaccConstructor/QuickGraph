using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    /// <summary>
    /// Disjoint-set implementation with path compression and union-by-rank optimizations.
    /// optimization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ForestDisjointSet<T>
        : IDisjointSet<T>
    {
        class Element
        {
            public Element Parent;
            public int Rank;
            public Element()
            {
                this.Parent = null;
                this.Rank = 0;
            }
        }

        readonly Dictionary<T, Element> elements;
        int setCount;

        public ForestDisjointSet(int elementCapacity)
        {
            Contract.Requires(elementCapacity >= 0);
            this.elements = new Dictionary<T, Element>(elementCapacity);
            this.setCount = 0;
        }

        public ForestDisjointSet()
        {
            this.elements = new Dictionary<T, Element>();
            this.setCount = 0;
        }

        public int SetCount
        {
            get { return this.setCount; }
        }

        public int ElementCount
        {
            get { return this.elements.Count; }
        }

        /// <summary>
        /// Adds a new set
        /// </summary>
        /// <param name="value"></param>
        public void MakeSet(T value)
        {
            Contract.Requires(value != null);

            var element = new Element();
            this.elements.Add(value, element);
            this.setCount++;
        }

        [Pure]
        public bool Contains(T value)
        {
            Contract.Requires(value != null);

            return this.elements.ContainsKey(value);
        }

        public void Union(T left, T right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Requires(this.Contains(left));
            Contract.Requires(this.Contains(right));

            this.Union(this.elements[left], this.elements[right]);
        }

        public object FindSet(T value)
        {
            Contract.Requires(value != null);
            Contract.Requires(this.Contains(value));

            return this.Find(this.elements[value]);
        }

        public bool AreInSameSet(T left, T right)
        {
            return this.FindSet(left) == this.FindSet(right);
        }

        private Element Find(Element element)
        {
            Contract.Requires(element != null);

            // find root,
            var current = element;
            while (current.Parent != null)
                current = current.Parent;

            var root = current;
            // path compression
            current = element;
            while (current.Parent != root)
            {
                var temp = current;
                current = current.Parent;
                temp.Parent = root;
            }

            return root;
        }

        private void Union(Element left, Element right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Ensures(left == right || Contract.OldValue(this.SetCount) - 1 == this.SetCount);

            // shortcut when already unioned,
            if (left == right) return;

            var leftRoot = Find(left);
            var rightRoot = Find(right);
            // union by rank
            if (leftRoot.Rank > rightRoot.Rank)
                 rightRoot.Parent = leftRoot;
             else if (leftRoot.Rank < rightRoot.Rank)
                 leftRoot.Parent = rightRoot;
            else if (leftRoot != rightRoot)
            {
                rightRoot.Parent = leftRoot;
                leftRoot.Rank = leftRoot.Rank + 1;
            }

            this.setCount--;
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.setCount >= 0);
            Contract.Invariant(this.setCount <= this.elements.Count);
        }
    }
}
