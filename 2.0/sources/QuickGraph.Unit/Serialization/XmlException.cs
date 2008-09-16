using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlException
    {
        private string exceptionType;
        private string message;
        private string stackTrace;
        private string source;
        private PropertyCollection properties = new PropertyCollection();
        private XmlException innerException = null;

        public XmlException()
        {}

        public static XmlException FromException(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");
            if (exception is System.Reflection.TargetInvocationException)
                return FromException(exception.InnerException);

            XmlException xex = new XmlException();
            xex.ExceptionType = exception.GetType().FullName;
            xex.Message = UnitSerializer.XmlSerializerEscapeWorkAround(exception.Message);
            xex.StackTrace = exception.StackTrace;
            xex.Source = exception.Source;
            if (exception.InnerException!=null)
                xex.InnerException = FromException(exception.InnerException);

            foreach (System.Collections.DictionaryEntry de in exception.Data)
            {
                xex.Properties.Add(de);
            }

            return xex;
        }

        [XmlAttribute]
        public string Source
        {
            get { return this.source; }
            set { this.source = value; }
        }

        [XmlAttribute]
        public string ExceptionType
        {
            get { return this.exceptionType; }
            set { this.exceptionType = value; }
        }

        [XmlAttribute]
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        [XmlElement]
        public string StackTrace
        {
            get { return this.stackTrace; }
            set { this.stackTrace = value; }
        }

        [XmlElement]
        public XmlException InnerException
        {
            get { return this.innerException; }
            set { this.innerException = value; }
        }

        [XmlArray("Properties")]
        [XmlArrayItem(typeof(XmlException.Property))]
        public PropertyCollection Properties
        {
            get { return this.properties;}
        }

        [Serializable]
        public sealed class Property
        {
            private string name;
            private string value;

            [XmlAttribute]
            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            [XmlAttribute]
            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        [Serializable]
        public sealed class PropertyCollection : List<Property>
        {
            public void Add(System.Collections.DictionaryEntry de)
            {
                Property p = new Property();
                p.Name = String.Format("{0}",de.Key);
                p.Value = String.Format("{0}",de.Value);
                this.Add(p);
            }
        }
    }
}
