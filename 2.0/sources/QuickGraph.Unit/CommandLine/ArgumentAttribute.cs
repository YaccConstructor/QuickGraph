using System;
using System.Xml.Serialization;
namespace QuickGraph.CommandLine
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ArgumentAttribute : Attribute
    {
        private string longName;
        private string shortName;
        private string description = "";

        public ArgumentAttribute()
        { }

        public ArgumentAttribute(
            string longName,
            string shortName,
            string description
            )
        {
            this.longName = longName;
            this.shortName = shortName;
            this.description = description;
        }

        public virtual bool IsDefault
        {
            get { return false; }
        }

        public string LongName
        {
            get { return this.longName; }
            set { this.longName = value; }
        }

        public string ShortName
        {
            get { return this.shortName; }
            set { this.shortName = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
