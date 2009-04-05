using System;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class RootVertexNotSpecifiedException 
        : QuickGraphException
    {
        public RootVertexNotSpecifiedException() { }
        public RootVertexNotSpecifiedException(string message) : base( message ) { }
        public RootVertexNotSpecifiedException(string message, System.Exception inner) : base( message, inner ) { }
#if !SILVERLIGHT
        protected RootVertexNotSpecifiedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
#endif
    }
}
