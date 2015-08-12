using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph;

namespace QuickGraph.Heap
{
    public sealed class GcType 
        : IEquatable<GcType>
    {
        public readonly int ID;
        public readonly string Name;
        public bool Root;
        public int Count;
        public int Size;
        private int genCount;
        private int genSum;

        internal GcType(int id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Root = false;
            this.Count = 0;
            this.Size = 0;
        }

        public bool Equals(GcType other)
        {
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

        public override bool Equals(object obj)
        {
            return (object)obj != null && this.Equals(obj as GcType);
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2:0.0}\t{3}",
                this.Count,
                FormatHelper.ToSize(this.Size),
                this.Gen,
                this.Name);
        }

        public double Gen
        {
            get
            {
                if (this.genCount == 0)
                    return -1;
                else
                    return this.genSum / (double)this.genCount;
            }
        }

        public void AddObjectGeneration(int gen)
        {
            this.genCount++;
            this.genSum += gen;
        }
    }

    public sealed class GcTypeCollection : QueryableList<GcType>
    {
        internal GcTypeCollection(int capacity)
            :base(capacity)
        {
        }
        internal GcTypeCollection(IEnumerable<GcType> types)
            : base(types)
        { }

        protected override QueryableList<GcType> Create(int capacity)
        {
            return new GcTypeCollection(capacity);
        }

        public GcTypeCollection SortSize()
        {
            GcTypeCollection clone = new GcTypeCollection(this);
            clone.Sort((left, right) => -left.Size.CompareTo(right.Size));
            return clone;
        }

        public GcTypeCollection SortCount()
        {
            GcTypeCollection clone = new GcTypeCollection(this);
            clone.Sort((left, right) => -left.Count.CompareTo(right.Count));
            return clone;
        }

        public GcTypeCollection SortGen()
        {
            GcTypeCollection clone = new GcTypeCollection(this);
            clone.Sort((left, right) => left.Gen.CompareTo(right.Gen));
            return clone;
        }

        public GcTypeCollection MinimumSize(int size)
        {
            GcTypeCollection clone = new GcTypeCollection(this.Count);
            foreach (GcType type in this)
                if (type.Size >= size)
                    clone.Add(type);
            clone.TrimExcess();
            return clone;
        }
    }

    public sealed class GcTypeEdge : Edge<GcType>
    {
        public int Count;

        internal GcTypeEdge(GcType source, GcType target)
            : base(source, target)
        { }

        public override string ToString()
        {
            return String.Format("{0}\t{1} -> {2}",
                this.Count,
                this.Source.Name,
                this.Target.Name);
        }
    }
}
