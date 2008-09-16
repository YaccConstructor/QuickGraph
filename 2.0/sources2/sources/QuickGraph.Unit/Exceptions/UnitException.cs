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
    public class UnitException : ApplicationException
    {
        public UnitException() { }
        public UnitException(string message) : base(message) { }
        public UnitException(string message, System.Exception inner) : base(message, inner) { }
        public UnitException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
