using System;
using System.IO;

namespace QuickGraph.Unit
{
    public static class FileAssert
    {
        public static void Display(string fileName, bool showLineNumbers)
        {
            Exists(fileName);
            using(StreamReader reader = new StreamReader(fileName))
            {
                string source = reader.ReadToEnd();
                if (showLineNumbers)
                    source = CompilerAssert.InsertLineNumbers(source);
                Console.WriteLine(source);
            }
        }

        public static void Exists(string fileName)
        {
            Assert.IsTrue(File.Exists(fileName),
                "{0} does not exist", fileName);
        }

        public static void AreEqual(string left, string right)
        {
            Exists(left);
            Exists(right);

            FileInfo leftInfo = new FileInfo(left);
            FileInfo rightInfo = new FileInfo(right);

            Assert.AreEqual(leftInfo.Length, rightInfo.Length,
                "File lengths are not equal between [{0}] and [{1}]",
                left, right);

            using (StreamReader leftReader = new StreamReader(left))
            using (StreamReader rightReader = new StreamReader(right))
            {
                StreamAssert.AreEqual(leftReader, rightReader);
            }
        }


    }
}

