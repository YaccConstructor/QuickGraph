using System;
using System.Reflection;
using System.IO;

namespace QuickGraph.CommandLine
{
    internal sealed class EnumArgumentParser : ArgumentParserBase
    {
        public EnumArgumentParser(
            IMember member, 
            ArgumentAttribute argument,
            bool isMultiple)
            :base(member,argument,isMultiple)
        {}

        public override bool Parse(object instance, string arg)
        {
            string value = this.ExtractValue(arg);
            if (value==null)
                return false;
            this.AssignValue(instance, Enum.Parse(this.ElementType,value,true));
            return true;
        }

        protected override string GetValidValues()
        {
            StringWriter sw = new StringWriter();
            sw.Write(":[");
            bool first = true;
            foreach (Object value in Enum.GetValues(this.ElementType))
            {
                if (!first)
                    sw.Write(",{0}",value);
                else
                {
                    sw.Write("{0}", value);
                    first = false;
                }
            }
            sw.Write("]");
            return sw.ToString();
        }
    }
}
