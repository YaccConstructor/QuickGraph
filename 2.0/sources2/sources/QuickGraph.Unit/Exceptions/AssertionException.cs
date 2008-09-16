using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Xml;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public class AssertionException : ApplicationException
    {
        public AssertionException() { }
        public AssertionException(string message) : base(message) { }
        public AssertionException(string message, System.Exception inner) : base(message, inner) { }
        public AssertionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
