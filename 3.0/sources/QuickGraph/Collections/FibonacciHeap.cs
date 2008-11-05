using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QuickGraph.Collections
{
    /// <summary>
    /// Specifies the order in which a Heap will Dequeue items.
    /// </summary>
    public enum HeapDirection
    {
        /// <summary>
        /// Items are Dequeued in Increasing order from least to greatest.
        /// </summary>
        Increasing,
        /// <summary>
        /// Items are Dequeued in Decreasing order, from greatest to least.
        /// </summary>
        Decreasing
    }
    
    internal static class LambdaHelpers
    {
        /// <summary>
        /// Performs an action on each item in a list, used to shortcut a "foreach" loop
        /// </summary>
        /// <typeparam name="T">Type contained in List</typeparam>
        /// <param name="collection">List to enumerate over</param>
        /// <param name="action">Lambda Function to be performed on all elements in List</param>
        internal static void ForEach<T>(this IList<T> collection, Action<T> action)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                action(collection[i]);
            }
        }
        /// <summary>
        /// Performs an action on each item in a list, used to shortcut a "foreach" loop
        /// </summary>
        /// <typeparam name="T">Type contained in List</typeparam>
        /// <param name="collection">List to enumerate over</param>
        /// <param name="action">Lambda Function to be performed on all elements in List</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> Collection)
        {
            Stack<T> newStack = new Stack<T>();
            Collection.ForEach(x => newStack.Push(x));
            return newStack;
        }
    }

    internal class FibonacciHeapLinkedList<TPriority, TValue> : IEnumerable<FibonacciHeapCell<TPriority, TValue>>
    {
        FibonacciHeapCell<TPriority, TValue> mFirst;
        FibonacciHeapCell<TPriority, TValue> mLast;

        public FibonacciHeapCell<TPriority, TValue> First
        {
            get
            {
                return mFirst;
            }
        }

        internal FibonacciHeapLinkedList()
        {
            mFirst = null;
            mLast = null; 
        }
        internal void MergeLists(FibonacciHeapLinkedList<TPriority, TValue> List)
        {
            if (List.First != null)
            {
                if (mLast != null)
                {
                    mLast.Next = List.mFirst;
                }
                List.mFirst.Previous = mLast;
                mLast = List.mLast;
                if (mFirst == null)
                {
                    mFirst = List.mFirst;
                }
            }
        }

        internal void AddLast(FibonacciHeapCell<TPriority, TValue> Node)
        {
            if (mLast != null)
            {
                mLast.Next = Node;
            }
            Node.Previous = mLast;
            mLast = Node;
            if (mFirst == null)
            {
                mFirst = Node;
            }
        }

        internal void Remove(FibonacciHeapCell<TPriority, TValue> Node)
        {
            if (Node.Previous != null)
            {
                Node.Previous.Next = Node.Next;
            }
            else if (mFirst == Node)
            {
                mFirst = Node.Next;
            }

            if (Node.Next != null)
            {
                Node.Next.Previous = Node.Previous;
            }
            else if (mLast == Node)
            {
                mLast = Node.Previous;
            }

            Node.Next = null;
            Node.Previous = null;
        }

        #region IEnumerable<FibonacciHeapNode<T,K>> Members

        public IEnumerator<FibonacciHeapCell<TPriority, TValue>> GetEnumerator()
        {
            var current = mFirst;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
            yield break;
        }

        #endregion


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }

    public class FibonacciHeapCell<TPriority, TValue>
    {
        /// <summary>
        /// Determines of a Node has had a child cut from it before
        /// </summary>
        internal bool Marked { get; set; }

        /// <summary>
        /// Determines the depth of a node
        /// </summary>
        internal int Degree { get; set; }

        public TPriority Priority { get; set; }
        public TValue Value { get; set; }
        public bool Removed { get; set; }
        internal FibonacciHeapLinkedList<TPriority, TValue> Children { get; set; }
        internal FibonacciHeapCell<TPriority, TValue> Parent { get; set; }
        internal FibonacciHeapCell<TPriority, TValue> Next { get; set; }
        internal FibonacciHeapCell<TPriority, TValue> Previous { get; set; }
    }
    public class FibonacciHeap<TPriority, TValue> : IEnumerable<FibonacciHeapCell<TPriority, TValue>>
    {
        public FibonacciHeap()
            : this(HeapDirection.Increasing, Comparer<TPriority>.Default.Compare)
        { }

        public FibonacciHeap(HeapDirection Direction)
            : this(Direction, Comparer<TPriority>.Default.Compare)
        { }
        
        public FibonacciHeap(HeapDirection Direction, Comparison<TPriority> priorityComparison)            
        {
            mNodes = new FibonacciHeapLinkedList<TPriority, TValue>();
            mDegreeToNode = new Dictionary<int, FibonacciHeapCell<TPriority, TValue>>();
            DirectionMultiplier = (short)(Direction == HeapDirection.Increasing ? 1 : -1);
            this.mDirection = Direction;
            this.priorityComparsion = priorityComparison;
            mCount = 0;
        }
        FibonacciHeapLinkedList<TPriority, TValue> mNodes;
        FibonacciHeapCell<TPriority, TValue> mNext;
        private short DirectionMultiplier;  //Used to control the direction of the heap, set to 1 if the Heap is increasing, -1 if it's decreasing
                                          //We use the approach to avoid unnessecary branches
        private Dictionary<int, FibonacciHeapCell<TPriority, TValue>> mDegreeToNode;
        private readonly Comparison<TPriority> priorityComparsion;
        private readonly HeapDirection mDirection;
        public HeapDirection Direction { get { return mDirection; } }
        private int mCount;
        public int Count { get { return mCount; } }
        //Draws the current heap in a string.  Marked Nodes have a * Next to them

        public string DrawHeap()
        {
            var lines = new List<string>();
            var lineNum = 0;
            var columnPosition = 0;
            var stack = mNodes.Select(x => new { Node = x, Level = 0 }).Reverse().ToStack();
            while (stack.Count > 0)
            {
                var currentcell = stack.Pop();
                lineNum = currentcell.Level;
                if (lines.Count <= lineNum)
                    lines.Add(String.Empty);
                var currentLine = lines[lineNum];
                currentLine = currentLine.PadRight(columnPosition, ' ');
                var nodeString = currentcell.Node.Priority.ToString() + (currentcell.Node.Marked ? "*" : "") + " ";
                currentLine += nodeString;
                if (currentcell.Node.Children != null && currentcell.Node.Children.First != null)
                {
                    currentcell.Node.Children.Reverse().ForEach(x => stack.Push(new { Node = x, Level = currentcell.Level + 1 }));
                }
                else
                {
                    columnPosition += nodeString.Length;
                }
                lines[lineNum] = currentLine;
            }
            return lines.Aggregate("", (a, b) => a + "\r\n" + b);
        }

        public FibonacciHeapCell<TPriority, TValue> Enqueue(TPriority Priority, TValue Value)
        {
            var newNode =
                new FibonacciHeapCell<TPriority, TValue>
                {
                    Priority = Priority,
                    Value = Value,
                    Marked = false,
                    Children = new FibonacciHeapLinkedList<TPriority, TValue>(),
                    Degree = 1,
                    Next = null,
                    Previous = null,
                    Parent = null,
                    Removed = false
                };
            //We don't do any book keeping or maintenance of the heap on Enqueue,
            //We just add this node to the end of the list of Heaps, updating the Next if required
            mNodes.AddLast(newNode);
            if (mNext == null || (priorityComparsion(newNode.Priority, mNext.Priority) * DirectionMultiplier) < 0)
            {
                mNext = newNode;
            }
            mCount++;
            return newNode;            
        }

        public void Delete(FibonacciHeapCell<TPriority, TValue> Node)
        {
            ChangeKeyInternal(Node, default(TPriority), true);
            Dequeue();            
        }

        public void ChangeKey(FibonacciHeapCell<TPriority, TValue> Node, TPriority NewKey)
        {            
            ChangeKeyInternal(Node, NewKey, false);            
        }

        private void ChangeKeyInternal(FibonacciHeapCell<TPriority, TValue> Node, TPriority NewKey, bool deletingNode)
        {
            var delta = Math.Sign(priorityComparsion(Node.Priority, NewKey));
            if (delta == 0)
                return;
            if (delta == DirectionMultiplier || deletingNode)
            {
                //New value is in the same direciton as the heap
                Node.Priority = NewKey;
                var parentNode = Node.Parent;
                if (parentNode != null && ((priorityComparsion(NewKey, Node.Parent.Priority) * DirectionMultiplier) < 0 || deletingNode))
                {
                    Node.Marked = false;
                    parentNode.Children.Remove(Node);
                    UpdateNodesDegree(parentNode);
                    Node.Parent = null;
                    mNodes.AddLast(Node);
                    //This loop is the cascading cut, we continue to cut
                    //ancestors of the node reduced until we hit a root 
                    //or we found an unmarked ancestor
                    while (parentNode.Marked && parentNode.Parent != null)
                    {
                        parentNode.Parent.Children.Remove(parentNode);
                        UpdateNodesDegree(parentNode);
                        parentNode.Marked = false;
                        mNodes.AddLast(parentNode);
                        var currentParent = parentNode;
                        parentNode = parentNode.Parent;
                        currentParent.Parent = null;
                    }
                    if (parentNode.Parent != null)
                    {
                        //We mark this node to note that it's had a child
                        //cut from it before
                        parentNode.Marked = true;
                    }
                }
                //Update next
                if (deletingNode || (priorityComparsion(NewKey, mNext.Priority) * DirectionMultiplier) < 0)
                {
                    mNext = Node;
                }
            }
            else
            {
                //New value is in opposite direction of Heap, cut all children violating heap condition
                Node.Priority = NewKey;
                var query = Node.Children.Where(x => (priorityComparsion(Node.Priority, x.Priority) * DirectionMultiplier) > 0);
                if (query != null)
                {
                    foreach (var child in query.ToList())
                    {
                        Node.Marked = true;
                        Node.Children.Remove(child);
                        child.Parent = null;
                        child.Marked = false;
                        mNodes.AddLast(child);
                        UpdateNodesDegree(Node);
                    }
                }
                UpdateNext();
            }
        }

        /// <summary>
        /// Updates the degree of a node, cascading to update the degree of the
        /// parents if nessecary
        /// </summary>
        /// <param name="parentNode"></param>
        private void UpdateNodesDegree(FibonacciHeapCell<TPriority, TValue> parentNode)
        {
            var oldDegree = parentNode.Degree;
            parentNode.Degree = parentNode.Children.First != null ? parentNode.Children.Max(x => x.Degree) + 1 : 1;
            FibonacciHeapCell<TPriority, TValue> degreeMapValue;
            if (oldDegree != parentNode.Degree)
            {
                if (mDegreeToNode.TryGetValue(oldDegree, out degreeMapValue) && degreeMapValue == parentNode)
                {
                    mDegreeToNode.Remove(oldDegree);
                }
                else if (parentNode.Parent != null)
                {
                    UpdateNodesDegree(parentNode.Parent);
                }
            }
        }

        public void Dequeue()
        {
            mNodes.Remove(mNext);
            mNext.Next = null;
            mNext.Parent = null;
            mNext.Previous = null;
            mNext.Removed = true;
            FibonacciHeapCell<TPriority, TValue> currentDegreeNode;
            if (mDegreeToNode.TryGetValue(mNext.Degree, out currentDegreeNode))
            {
                if (currentDegreeNode == mNext)
                {
                    mDegreeToNode.Remove(mNext.Degree);
                }
            }
            foreach (var child in mNext.Children)
            {
                child.Parent = null;
            }
            mNodes.MergeLists(mNext.Children);
            mNext.Children = null;
            mCount--;
            UpdateNext();

        }

        /// <summary>
        /// Updates the Next pointer, maintaining the heap
        /// by folding duplicate heap degrees into eachother
        /// Takes O(lg(N)) time amortized
        /// </summary>
        private void UpdateNext()
        {
            CompressHeap();
            var node = mNodes.First;
            mNext = mNodes.First;
            while (node != null)
            {
                if ((priorityComparsion(node.Priority, mNext.Priority) * DirectionMultiplier) < 0)
                {
                    mNext = node;
                }
                node = node.Next;
            }
        }

        private void CompressHeap()
        {
            var node = mNodes.First;
            FibonacciHeapCell<TPriority, TValue> currentDegreeNode;
            while (node != null)
            {
                var nextNode = node.Next;
                while (mDegreeToNode.TryGetValue(node.Degree, out currentDegreeNode) && currentDegreeNode != node)
                {
                    mDegreeToNode.Remove(node.Degree);
                    if ((priorityComparsion(currentDegreeNode.Priority, node.Priority) * DirectionMultiplier) <= 0)
                    {
                        if (node == nextNode)
                        {
                            nextNode = node.Next;
                        }
                        ReduceNodes(currentDegreeNode, node);
                        node = currentDegreeNode;
                    }
                    else
                    {
                        if (currentDegreeNode == nextNode)
                        {
                            nextNode = currentDegreeNode.Next;
                        }
                        ReduceNodes(node, currentDegreeNode);
                    }
                }
                mDegreeToNode[node.Degree] = node;
                node = nextNode;
            }
        }

        /// <summary>
        /// Given two nodes, adds the child node as a child of the parent node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNode"></param>
        private void ReduceNodes(FibonacciHeapCell<TPriority, TValue> parentNode, FibonacciHeapCell<TPriority, TValue> childNode)
        {
            mNodes.Remove(childNode);
            parentNode.Children.AddLast(childNode);
            childNode.Parent = parentNode;
            childNode.Marked = false;
            if (parentNode.Degree == childNode.Degree)
            {
                parentNode.Degree += 1;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return mNodes.First == null;
            }
        }
        public FibonacciHeapCell<TPriority, TValue> Top
        {
            get
            {
                return mNext;
            }
        }

        public void Merge(FibonacciHeap<TPriority, TValue> Other)
        {            
            if (Other.Direction != this.Direction)
            {
                throw new Exception("Error: Heaps must go in the same direction when merging");
            }
            mNodes.MergeLists(Other.mNodes);
            if ((priorityComparsion(Other.Top.Priority, mNext.Priority) * DirectionMultiplier) < 0)
            {
                mNext = Other.mNext;
            }
            mCount += Other.Count;
        }

        public IEnumerator<FibonacciHeapCell<TPriority, TValue>> GetEnumerator()
        {
            var tempHeap = new FibonacciHeap<TPriority, TValue>(this.Direction, this.priorityComparsion);
            var NodeStack = new Stack<FibonacciHeapCell<TPriority, TValue>>();
            mNodes.ForEach(x => NodeStack.Push(x));
            while (NodeStack.Count > 0)
            {
                var topNode = NodeStack.Peek();
                tempHeap.Enqueue(topNode.Priority, topNode.Value);
                NodeStack.Pop();
                topNode.Children.ForEach(x => NodeStack.Push(x));
            }
            while (!tempHeap.IsEmpty)
            {
                yield return tempHeap.Top;
                tempHeap.Dequeue();
            }
        }
        public IEnumerable<FibonacciHeapCell<TPriority, TValue>> GetDestructiveEnumerator()
        {
            while (!this.IsEmpty)
            {
                yield return this.Top;
                this.Dequeue();
            }
        }

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}