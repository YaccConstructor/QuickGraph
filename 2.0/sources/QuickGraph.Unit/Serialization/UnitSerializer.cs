using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading;

namespace QuickGraph.Unit.Serialization
{
    public static class UnitSerializer
    {
        private static volatile object syncRoot = new object();
        private static XmlSerializer testBatchSerializer = null;

        public static object SyncRoot
        {
            get { return syncRoot; }
        }

        public static string XmlSerializerEscapeWorkAround(string s)
        {
            char[] array = s.ToCharArray();
            for (int i = 0; i < s.Length; ++i)
            {
                //#x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]
                char c = array[i];
                if (
                    !(
                      (c>=0x20 && c<=0xD7FF)
                    ||(c>=0xE000 && c<=0xFFFD)
                    || c == 0x9 || c==0xA || c==0xD
                    )
                    )
                    array[i] = 'x';
            }
            return new string(array);
        }

        public static XmlSerializer TestBatchSerializer
        {
            get
            {
                lock (SyncRoot)
                {
                    if (testBatchSerializer == null)
                        testBatchSerializer = new XmlSerializer(typeof(XmlTestBatch));
                    return testBatchSerializer;
                }
            }
        }

        public static void Serialize(XmlTestBatch instance, string outputFileName)
        {
            using (StreamWriter sw = new StreamWriter(outputFileName))
            {
                using (XmlTextWriter xw = new XmlTextWriter(sw))
                {
                    xw.Formatting = Formatting.Indented;
                    TestBatchSerializer.Serialize(xw, instance);
                }
            }
        }

        public static XmlTestBatch Deserialize(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return (XmlTestBatch)TestBatchSerializer.Deserialize(reader);
            }
        }
    }
}
