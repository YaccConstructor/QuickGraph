using System;
using System.IO;

namespace QuickGraph.Unit
{
    public static class StreamAssert
    {
        public static void AreEqual(Stream leftStream, Stream rightStream)
        {
            Assert.AreEqual(leftStream.CanRead, rightStream.CanRead,
                "CanRead property is not the same");
            Assert.AreEqual(leftStream.CanSeek, rightStream.CanSeek,
                "CanSeek property is not the same");
            Assert.AreEqual(leftStream.CanTimeout, rightStream.CanTimeout,
                "CanTimeOut property is not the same");
            Assert.AreEqual(leftStream.CanWrite, rightStream.CanWrite,
                "CanWriteProperty is not the same");

            if (leftStream.CanRead)
            {
                using (StreamReader leftReader = new StreamReader(leftStream))
                using (StreamReader rightReader = new StreamReader(rightStream))
                {
                    AreEqual(leftReader, rightReader);
                }
            }
        }

        public static void AreEqual(StreamReader leftReader, StreamReader rightReader)
        {
            Assert.AreEqual(leftReader.EndOfStream, rightReader.EndOfStream,
                "The end of the streams do not match");
            int lineNumber = 0;
            while (!leftReader.EndOfStream)
            {
                string leftLine = leftReader.ReadLine();
                string rightLine = rightReader.ReadLine();
                Assert.AreEqual(leftLine, rightLine,
                    "Line {0} are different: [[{1}]], [[{2}]]",
                    lineNumber, leftLine, rightLine);
                Assert.AreEqual(leftReader.EndOfStream, rightReader.EndOfStream,
                "The end of the streams do not match");
                lineNumber++;
            }
        }
    }
}
