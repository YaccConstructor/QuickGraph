using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph;
using System.IO;
using System.Globalization;

namespace QuickGraph.Heap
{
    sealed class GcTypeGraphReader 
        : GcHeapXmlReader
    {
        readonly BidirectionalGraph<GcType, GcTypeEdge> graph = new BidirectionalGraph<GcType, GcTypeEdge>();
        // id -> type
        readonly Dictionary<int, GcType> types = new Dictionary<int, GcType>();
        // adress -> root
        readonly Dictionary<int, GcRoot> roots = new Dictionary<int,GcRoot>();
        // adress -> type
        readonly Dictionary<int, GcType> objectTypes = new Dictionary<int, GcType>();
        readonly List<GcMember> unresolvedMembers = new List<GcMember>();
        // GC heap layout
        int gen0Start = -1;
        int gen1Start = -1;
        int gen2Start = -1;
        int loStart = -1;

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
            // updating gen
            if (gen0Start > -1)
            {
                if (address >= gen0Start && address < gen1Start)
                    this.current.AddObjectGeneration(0);
                else if (address < gen2Start)
                    this.current.AddObjectGeneration(1);
                else if (address < loStart)
                    this.current.AddObjectGeneration(2);
                else
                    this.current.AddObjectGeneration(3);
            }
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

        public void ParseEEHeap(string content)
        {
            /* 0:011> !eeheap -gc
*********************************************************************
* Symbols can not be loaded because symbol path is not initialized. *
*                                                                   *
* The Symbol Path can be set by:                                    *
*   using the _NT_SYMBOL_PATH environment variable.                 *
*   using the -y <symbol_path> argument when starting the debugger. *
*   using .sympath and .sympath+                                    *
*********************************************************************
PDB symbol for mscorwks.dll not loaded
Number of GC Heaps: 1
generation 0 starts at 0x01cdc384
generation 1 starts at 0x01cdc378
generation 2 starts at 0x01c01000
ephemeral segment allocation context: none
 segment    begin allocated     size
01c00000 01c01000  01e823a8 0x002813a8(2626472)
Large object heap starts at 0x02c01000
 segment    begin allocated     size
02c00000 02c01000  02c07de8 0x00006de8(28136)*/
            ParseLong(content, "generation 0 starts at 0x", out gen0Start);
            ParseLong(content, "generation 1 starts at 0x", out gen1Start);
            ParseLong(content, "generation 2 starts at 0x", out gen2Start);
            ParseLong(content, "Large object heap starts at 0x", out loStart);

            Console.WriteLine("gen 0: {0:x}", gen0Start);
            Console.WriteLine("gen 1: {0:x}", gen1Start);
            Console.WriteLine("gen 2: {0:x}", gen2Start);
            Console.WriteLine("large objects: {0:x}", loStart);
        }

        private static void ParseLong(string content, string prefix, out int value)
        {
            int index = content.IndexOf(prefix);
            if (index > -1)
            {
                index = index + prefix.Length;
                int endIndex = content.IndexOfAny(new char[] {' ', '\n'}, index);
                if (endIndex > -1)
                {
                    var svalue = content.Substring(index, endIndex - index);
                    Console.WriteLine("{0} -> '{1}'", prefix, svalue);
                    if (int.TryParse(svalue, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                        return;
                    Console.WriteLine("failed parsing range " + svalue);
                }
            }

            value = -1;
        }
    }
}
