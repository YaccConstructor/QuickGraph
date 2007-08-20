using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Unit;
using QuickGraph.Heap;

namespace QuickGraph.Tests.Heap
{
    [TestFixture]
    public partial class HeapTest
    {
        [Test]
        public void Roots()
        {
            GcTypeHeap heap = GcTypeHeap.Load("gcheap.xml");

            Console.WriteLine(heap.Roots);
        }

        [Test]
        public void Types()
        {
            GcTypeHeap heap = GcTypeHeap.Load("gcheap.xml");

            Console.WriteLine(heap.Types);
        }

        [Test]
        public void Size()
        {
            GcTypeHeap heap = GcTypeHeap.Load("gcheap.xml");

            Console.WriteLine(heap.Size);
        }

        [Test]
        public void Touching()
        {
            GcTypeHeap heap = GcTypeHeap.Load("gcheap.xml");
            Console.WriteLine(heap.Touching("Byte").Types);
        }

        [Test]
        public void Merge()
        {
            GcTypeHeap heap = GcTypeHeap.Load("gcheap.xml");
            Console.WriteLine(heap.Merge(1000).Types);
        }
    }
}
