using System;

namespace QuickGraph
{
    /// <summary>
    /// Specialized exception to report unconnected vertices
    /// </summary>
    [Serializable]
    public sealed class VertexNotConnectedException : ApplicationException
    {
        public VertexNotConnectedException() { }
        public VertexNotConnectedException(string message) : base( message ) { }
        public VertexNotConnectedException(string message, System.Exception inner) : base( message, inner ) { }
        public VertexNotConnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
