using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph;
using System.IO;

namespace QuickGraph.Heap
{
    internal sealed class GcTypeGraphReader : GcHeapXmlReader
    {
        private readonly BidirectionalGraph<GcType, GcTypeEdge> graph = new BidirectionalGraph<GcType, GcTypeEdge>();
        private readonly Dictionary<int, GcType> types = new Dictionary<int, GcType>();
        private readonly Dictionary<int, GcRoot> roots = new Dictionary<int,GcRoot>();
        private readonly Dictionary<int, GcType> objectTypes = new Dictionary<int, GcType>();
        private readonly List<GcMember> unresolvedMembers = new List<GcMember>();

        public GcTypeGraphReader()
        {}

        public BidirectionalGraph<GcType, GcTypeEdge> Graph
        {
            get { return this.graph; }
        }

        // remeber type and roots
        protected override void VisitType(int id, string name)
        {
            GcType type = new GcType(id, name);
            this.types.Add(type.ID, type);
            this.graph.AddVertex(type);
        }

        protected override void VisitRoot(string kind, int address)
        {
            if (kind == "stack") return; // skip stack roots
            this.roots[address] = new GcRoot(kind, address);
        }

        GcType current;
        protected override void VisitStartObject(int address, int typeid, int size)
        {
            if (!this.types.TryGetValue(typeid, out this.current))
                throw new InvalidOperationException("unknown typeid " + typeid);

            this.current.Count++;
            this.current.Size += size;
            if (!this.current.Root) // lazy check if rooted
                this.current.Root = this.roots.ContainsKey(address);

            this.objectTypes.Add(address, this.current);
        }

        protected override void VisitMember(int address)
        {
            GcType target;
            if (!this.objectTypes.TryGetValue(address, out target))
            {
                // object type not loaded yet, 
                // store member 
                this.unresolvedMembers.Add(new GcMember(this.current, address));
            }
            else
            {
                AddTypeEdge(target, this.current);
            }
        }

        private void AddTypeEdge(GcType source, GcType target)
        {
            GcTypeEdge edge;
            if (!this.graph.TryGetEdge(source, target, out edge))
                this.graph.AddEdge(edge = new GcTypeEdge(source, target));
            edge.Count++;
        }

        // process unresolved members
        protected override void VisitEndObjects()
        {
            FlushUnresolvedMembers();
        }

        private void FlushUnresolvedMembers()
        {
            foreach (GcMember member in this.unresolvedMembers)
            {
                GcType target = member.Referer;
                GcType source;
                if (this.objectTypes.TryGetValue(member.Address, out source))
                    this.AddTypeEdge(source, target);
            }
            this.unresolvedMembers.Clear();
        }

        private void ParseDump(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            using (StreamReader reader = new StreamReader(fileName))
                while (!reader.EndOfStream)
                    this.ParseDumpLine(reader.ReadLine());
        }

        private static IEnumerable<string> SplitLine(string line)
        {
            foreach (string item in line.Split(' '))
            {
                string trimmed = item.Trim();
                if (!String.IsNullOrEmpty(trimmed))
                    yield return trimmed;
            }
        }

        private void ParseDumpLine(string line)
        {
            if (line == null)
                return;
            string l = line.Trim();
            if (string.IsNullOrEmpty(l))
                return;

            // this should contain the address
            List<string> elements = new List<string>(SplitLine(l));
            if (!elements[0].ToLowerInvariant().StartsWith("0x"))
                return;
            int address = int.Parse(elements[0].Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);

            // element[3] contains the gen
            int gen;
            if (!int.TryParse(elements[3], out gen))
                return;

            // we can update the object
            // TODO
            //GcObjectVertex v = this.ObjectGraph.FromAddress(address);
            //if (v == null)
            //    return;

            //v.Gen = gen;
        }
    }
}
