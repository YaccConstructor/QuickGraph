namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Queue<T> : 
        System.Collections.Generic.Queue<T>,
        IQueue<T>
    {
    }
}
