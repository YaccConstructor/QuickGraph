using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;

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
#if DEBUG
        [DebuggerDisplay("{ID}:{Rank}->{Parent}")]
#endif
        class Element
        {
#if DEBUG
            public readonly int ID;
            static int nextID;
#endif
            public Element Parent;
            public int Rank;
            public Element()
            {
#if DEBUG
                this.ID = nextID++;
#endif
                this.Parent = null;
                this.Rank = 0;
            }
        }

        readonly Dictionary<T, Element> elements;
        int setCount;

        public ForestDisjointSet(int elementCapacity)
        {
            Contract.Requires(elementCapacity >= 0 && elementCapacity < int.MaxValue);
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
            Contract.Ensures(this.FindNoCompression(this.elements[left]) == this.FindNoCompression(this.elements[right]));

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

        [Pure]
        private Element FindNoCompression(Element element)
        {
            Contract.Requires(element != null);
            Contract.Ensures(Contract.Result<Element>() != null);

            // find root,
            var current = element;
            while (current.Parent != null)
                current = current.Parent;

            return current;
        }

        /// <summary>
        /// Finds the parent element, and applies path compression
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private Element Find(Element element)
        {
            Contract.Requires(element != null);
            Contract.Ensures(Contract.Result<Element>() != null);

            var root = this.FindNoCompression(element);            
            CompressPath(element, root);
            return root;
        }

        private static void CompressPath(Element element, Element root)
        {
            Contract.Requires(element != null);
            Contract.Requires(root != null);

            // path compression
            var current = element;
            while (current != root)
            {
                var temp = current;
                current = current.Parent;
                temp.Parent = root;
            }
        }

        private void Union(Element left, Element right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Ensures(left == right || Contract.OldValue(this.SetCount) - 1 == this.SetCount);
            Contract.Ensures(this.FindNoCompression(left) == this.FindNoCompression(right));

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
            else
                return; // do not update the setcount

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
