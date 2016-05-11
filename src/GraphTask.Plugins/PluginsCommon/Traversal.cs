using System.Collections.Generic;
using QuickGraph.GraphXAdapter;

namespace PluginsCommon
{
    public class Traversal
    {
        public class Node
        {
            public LinkedList<Node> children = new LinkedList<Node>();
            public GraphXVertex v;
            public Node parent;

            public Node(GraphXVertex v, Node parent)
            {
                this.parent = parent;
                this.v = v;
            }

            public Node AddChild(GraphXVertex v)
            {
                Node child = new Node(v, this);
                children.AddLast(child);
                return child;
            }
        }
        public Node currNode;
        private LinkedList<Node> nodes = new LinkedList<Node>();
        public Traversal()
        { }

        public void Push(GraphXVertex v)
        {
            if (currNode == null)
            {
                currNode = new Node(v, null);
                nodes.AddLast(currNode);
            }
            else
                currNode = currNode.AddChild(v);
        }

        public void Pop()
        {
            if (currNode == null)
                currNode = null;
            else
                currNode = currNode.parent;
        }

    }
}
