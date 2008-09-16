namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class Queue<T> : 
        System.Collections.Generic.Queue<T>,
        IQueue<T>
    {
    }
}
