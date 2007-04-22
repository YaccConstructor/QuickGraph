using System;
using System.Xml.Serialization;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlCounter
    {
        private int totalCount = 0;
        private int successCount = 0;
        private int failureCount = 0;
        private int ignoreCount = 0;
        private int notRunCount = 0;

        public XmlCounter()
        { }

        public static XmlCounter Add(XmlCounter left, XmlCounter right)
        {
            XmlCounter counter = new XmlCounter();
            counter.TotalCount = left.TotalCount + right.TotalCount;
            counter.SuccessCount = left.SuccessCount + right.SuccessCount;
            counter.FailureCount = left.FailureCount + right.FailureCount;
            counter.IgnoreCount = left.IgnoreCount + right.IgnoreCount;
            counter.notRunCount = left.notRunCount + right.notRunCount;
            return counter;
        }

        public void Update()
        {
            this.totalCount = this.successCount + this.ignoreCount + this.failureCount + this.notRunCount;
        }

        public static XmlCounter operator +(XmlCounter left, XmlCounter right)
        {
            return Add(left, right);
        }

        [XmlAttribute]
        public int TotalCount
        {
            get { return this.totalCount; }
            set { this.totalCount = value; }
        }

        [XmlAttribute]
        public int SuccessCount
        {
            get { return this.successCount; }
            set { this.successCount = value; }
        }

        [XmlAttribute]
        public int FailureCount
        {
            get { return this.failureCount; }
            set { this.failureCount = value; }
        }

        [XmlAttribute]
        public int IgnoreCount
        {
            get { return this.ignoreCount; }
            set { this.ignoreCount = value; }
        }

        [XmlAttribute]
        public int NotRunCount
        {
            get { return this.notRunCount; }
            set { this.notRunCount = value; }
        }

        public override string ToString()
        {
            return String.Format("{0} tests, {1} success, {2} failures, {3} ignored",
                this.TotalCount,
                this.SuccessCount,
                this.FailureCount,
                this.IgnoreCount);
        }
    }
}
