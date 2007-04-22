using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
    internal sealed class FloatArgumentParser : ArgumentParserBase
    {
        public FloatArgumentParser(
            IMember member,
            ArgumentAttribute argument,
            bool isMultiple)
            : base(member, argument, isMultiple)
        { }

        public override bool Parse(object instance, string arg)
        {
            string value = this.ExtractValue(arg);
            if (value == null)
                return false;
            this.AssignValue(instance, float.Parse(value));
            return true;
        }
    }
}
