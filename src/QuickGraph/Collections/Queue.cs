namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class Queue<T> : 
        System.Collections.Generic.Queue<T>,
        IQueue<T>
    {
    }
}
