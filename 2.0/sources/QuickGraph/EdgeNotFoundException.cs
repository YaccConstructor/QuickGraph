using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{

    [global::System.Serializable]
    public class EdgeNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public EdgeNotFoundException() { }
        public EdgeNotFoundException(string message) : base(message) { }
        public EdgeNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EdgeNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
