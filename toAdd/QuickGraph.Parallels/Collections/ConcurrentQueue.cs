using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph.Collections
{
    public sealed class ConcurrentQueue<T> 
        : System.Threading.Collections.ConcurrentQueue<T>
        , IQueue<T>        
    {
        bool IQueue<T>.Contains(T value)
        {
            foreach (var item in this)
                if (item.Equals(value))
                    return true;
            return false;
        }

        T IQueue<T>.Dequeue()
        {
            T result;
            if (!this.TryDequeue(out result))
                throw new InvalidOperationException();
            return result;
        }

        T IQueue<T>.Peek()
        {
            T result;
            if (!this.TryPeek(out result))
                throw new InvalidOperationException();
            return result;
        }
    }
}
