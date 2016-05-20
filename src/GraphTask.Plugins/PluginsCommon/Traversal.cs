using System.Collections.Generic;
using QuickGraph.GraphXAdapter;

namespace PluginsCommon
{
    public class Traversal<TVertex>
    {
        public class Node
        {
            public List<Node> children = new List<Node>();
            public TVertex v;
            public Node parent;

            public Node(TVertex v, Node parent)
            {
                this.parent = parent;
                this.v = v;
            }

            public Node AddChild(TVertex v)
            {
                Node child = new Node(v, this);
                children.Add(child);
                return child;
            }
        }
        public Node currNode;
        public List<Node> nodes = new List<Node>();
        public Traversal()
        { }

        public void Push(TVertex v)
        {
            if (currNode == null)
            {
                currNode = new Node(v, null);
                nodes.Add(currNode);
            }
            else
                currNode = currNode.AddChild(v);
        }

        public void Pop()
        {
            if (currNode != null)
                currNode = currNode.parent;                
        }

        public void Clear()
        {
            currNode = null;
            nodes.Clear();
        }

    }
}
