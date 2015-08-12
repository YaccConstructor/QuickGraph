using System;
using System.Collections.Generic;
using System.IO;

namespace QuickGraph.Heap.Data
{
    public sealed class GcHeapParser
    {
        private readonly GcObjectGraph objectGraph;

        public GcHeapParser(GcObjectGraph objectGraph)
        {
            this.objectGraph = objectGraph;
        }

        public GcObjectGraph ObjectGraph
        {
            get { return this.objectGraph; }
        }

        public void Parse(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    ParseLine(line);
                }
            }
        }

        private IEnumerable<string> SplitLine(string line)
        {
            foreach (string item in line.Split(' '))
            {
                string trimmed = item.Trim();
                if (!String.IsNullOrEmpty(trimmed))
                    yield return trimmed;
            }
        }

        public void ParseLine(string line)
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
            GcObjectVertex v = this.ObjectGraph.FromAddress(address);
            if (v == null)
                return;

            v.Gen = gen;
        }
    }
}
