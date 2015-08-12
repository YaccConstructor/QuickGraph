using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace QuickGraph.Heap
{
    struct GcRoot
    {
        public readonly string Kind;
        public readonly int Address;
        public GcRoot(string kind, int address)
        {
            this.Kind = kind;
            this.Address = address;
        }
    }

    struct GcMember
    {
        public readonly GcType Referer;
        public readonly int Address;
        public GcMember(GcType referer, int address)
        {
            this.Referer = referer;
            this.Address = address;
        }
    }

    abstract class GcHeapXmlReader
    {
        public void Read(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        this.ReadElement(reader);
                        break;
                    case XmlNodeType.EndElement:
                        this.ReadEndElement(reader);
                        break;
                    default: // skip the rest
                        break;
                }
            }
        }

        private void ReadElement(XmlReader reader)
        {
            switch (reader.Name)
            {
                case "gcheap":
                    // top level element, continue
                    this.VisitStartGcHeap();
                    break;
                case "types":
                    // top level types,
                    this.VisitStartTypes();
                    break;
                case "type":
                    {
                        int id;
                        // type element
                        if (!int.TryParse(reader.GetAttribute("id"), out id))
                            throw new XmlException("could not parse gcheap/types/type/@id");
                        string name = reader.GetAttribute("name");
                        this.VisitType(id, name);

                        // move to next
                        reader.Skip();
                        break;
                    }
                case "roots":
                    this.VisitStartRoots();
                    break;
                case "root":
                    {
                        string kind = reader.GetAttribute("kind");
                        int address;
                        if (!TryParseAddress(reader.GetAttribute("address"), out address))
                            throw new XmlException("could not parse gcheap/roots/root/@address");

                        this.VisitRoot(kind, address);

                        // move to next
                        reader.Skip();
                        break;
                    }
                case "objects":
                    this.VisitStartObjects();
                    break;
                case "object":
                    {
                        int size;
                        int typeid;
                        int address;
                        if (!TryParseAddress(reader.GetAttribute("address"), out address))
                            throw new XmlException("could not parse gcheap/objects/object/@address");
                        if (!int.TryParse(reader.GetAttribute("typeid"), out typeid))
                            throw new XmlException("could not parse gcheap/objects/object/@typeid");
                        if (!int.TryParse(reader.GetAttribute("size"), out size))
                            throw new XmlException("could not parse gcheap/objects/object/@size");
                        this.VisitStartObject(address, typeid, size);
                        break;
                    }
                case "member":
                    {
                        int address;
                        if (!TryParseAddress(reader.GetAttribute("address"), out address))
                            throw new XmlException("could not parse gcheap/objects/object/member/@address");
                        this.VisitMember(address);
                        // read node
                        reader.Skip();
                        break;
                    }
                default:
                    throw new XmlException("unknown element " + reader.Name);
            }
        }

        private void ReadEndElement(XmlReader reader)
        {
            switch (reader.Name)
            {
                case "gcheap":
                    // top level element, continue
                    this.VisitEndGcHeap();
                    break;
                case "types":
                    // top level types,
                    this.VisitEndTypes();
                    break;
                case "roots":
                    this.VisitEndRoots();
                    break;
                case "objects":
                    this.VisitEndObjects();
                    break;
                case "object":
                    this.VisitEndObject();
                    break;
                case "member":
                default:
                    throw new XmlException("unexpected end element " + reader.Name);
            }
        }

        protected static bool TryParseAddress(string value, out int address)
        {
            address = 0;
            return
                value != null &&
                value.StartsWith("0x") &&
                int.TryParse(value.Substring(2), NumberStyles.HexNumber, null, out address);
        }

        protected virtual void VisitStartGcHeap() {}

        protected virtual void VisitEndGcHeap() {}

        protected virtual void VisitStartTypes() {}

        protected virtual void VisitEndTypes() {}

        protected virtual void VisitType(int id, string name) {}

        protected virtual void VisitRoot(string kind, int address) {}

        protected virtual void VisitEndRoots() {}

        protected virtual void VisitStartRoots() {}

        protected virtual void VisitStartObjects() {}

        protected virtual void VisitEndObjects() {}

        protected virtual void VisitStartObject(int address, int typeid, int size) {}

        protected virtual void VisitEndObject() { }

        protected virtual void VisitMember(int address) {}
    }
}
