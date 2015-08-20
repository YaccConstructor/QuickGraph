using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
    internal sealed class StringArgumentParser : ArgumentParserBase
    {
        public StringArgumentParser(
            IMember member,
            ArgumentAttribute argument,
            bool isMultiple)
            :base(member,argument,isMultiple)
        {}

        public override bool Parse(object instance, string arg)
        {
            string value = this.ExtractValue(arg);
            if (value == null)
                return false;
            value = value.Trim('"');
            this.AssignValue(instance, value);
            return true;
        }
    }
}
