using System;
using System.Collections;

namespace QuickGraph.Operations
{
    public sealed class CollectionDomain : CollectionBase, IDomain
    {
        private string name = null;

        public CollectionDomain()
        { }

        public CollectionDomain(ICollection collection)
        {
            foreach (Object item in collection)
                this.List.Add(item);
        }
        public CollectionDomain(IEnumerable enumerable)
        {
            foreach(Object item in enumerable)
                this.List.Add(item);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public object this[int i]
        {
            get
            {
                return this.List[i];
            }
        }

        public void AddDomain(IDomain domain)
        {
            if (domain == null)
                throw new ArgumentNullException("domain");
            this.AddRange(domain);
        }

        public void AddRange(IEnumerable enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            foreach (Object o in enumerable)
                this.List.Add(o);
        }
    }
}
