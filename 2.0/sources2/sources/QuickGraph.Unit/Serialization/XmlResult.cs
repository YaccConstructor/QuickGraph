using System;
using System.IO;
using System.Xml.Serialization;
using QuickGraph.Unit.Monitoring;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public class XmlResult : XmlMonitor
    {
        private string id;
        private TestState state = TestState.NotRun;
        private XmlException exception = null;

        public XmlResult()
        { }

        public XmlResult(Result result)
            : base(result.Monitor)
        {
            this.Update(result);
        }

        public void Update(Result result)
        {
            if (result == null)
                throw new ArgumentNullException("result");
            this.state = result.State;
            if (result.Exception != null)
                this.exception = XmlException.FromException(result.Exception);
            this.Update(result.Monitor);
        }

        [XmlAttribute("id")]
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [XmlAttribute]
        public TestState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        [XmlElement]
        public XmlException Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }
    }
}
