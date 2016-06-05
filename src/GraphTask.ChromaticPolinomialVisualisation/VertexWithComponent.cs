using System;
using QuickGraph.GraphXAdapter;

namespace ChromaticPolinomialVisualisation
{
    enum Component
    {
        Parent, LeftChild, RightChild, TreeParent, TreeLeftChild, TreeRightChild, TreeRoot, TreeRest, Changed
    }
    class VertexWithComponent : GraphXVertex
    {
        public VertexWithComponent(String text, Component component) : base(text)
        {
            Component = component;
        }

        public Component Component { get; set; }
    }
}
