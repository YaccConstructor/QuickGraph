using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlFixtureCollection : List<XmlFixture>
    {
        private XmlCounter counter = new XmlCounter();

        [XmlElement]
        public XmlCounter Counter
        {
            get { return this.counter; }
            set { this.counter = value; }
        }
    }
}
