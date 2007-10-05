using System;

namespace QuickGraph
{
    [System.Serializable]
    public class RootVertexNotSpecifiedException : System.ApplicationException
    {
        public RootVertexNotSpecifiedException() { }
        public RootVertexNotSpecifiedException(string message) : base( message ) { }
        public RootVertexNotSpecifiedException(string message, System.Exception inner) : base( message, inner ) { }
        protected RootVertexNotSpecifiedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
