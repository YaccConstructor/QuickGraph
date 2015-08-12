import clr
clr.AddReferenceToFile("QuickGraph.Heap")
import QuickGraph.Heap
from QuickGraph.Heap import GcTypeHeap
g = GcTypeHeap.Load("heap.xml", "eeheap.txt")
g = g.Merge(20)