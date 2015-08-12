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
        private GcTypeHeap LoadHeap()
        {
            GcTypeHeap heap = GcTypeHeap.Load(@"heap\gcheap.xml");
            return heap;
        }
        [Test]
        public void Roots()
        {
            GcTypeHeap heap = this.LoadHeap();
            Console.WriteLine(heap.Roots);
        }

        [Test]
        public void Types()
        {
            GcTypeHeap heap = this.LoadHeap();
            Console.WriteLine(heap.Types);
        }

        [Test]
        public void Size()
        {
            GcTypeHeap heap = this.LoadHeap();
            Console.WriteLine(heap.Size);
        }

        [Test]
        public void Touching()
        {
            GcTypeHeap heap = this.LoadHeap();
            Console.WriteLine(heap.Touching("Byte").Types);
        }

        [Test]
        public void Merge()
        {
            GcTypeHeap heap = this.LoadHeap();
            Console.WriteLine(heap.Merge(1000).Types);
        }
    }
}
