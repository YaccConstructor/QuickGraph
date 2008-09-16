using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuickGraph.CommandLine
{
    internal abstract class ArgumentParserBase : IArgumentParser
    {
        private string flags = "/-";
        private IMember member;
        private ArgumentAttribute argument;
        private bool isMultiple;

        public ArgumentParserBase(
            IMember member, 
            ArgumentAttribute argument,
            bool isMultiple)
        {
			if (member == null)
				throw new ArgumentNullException("member");
			if (argument==null)
				throw new ArgumentNullException("argument");
			this.member = member;
            this.argument = argument;
            this.isMultiple = isMultiple;
        }

        public IMember Member
        {
            get { return this.member; }
        }
        public ArgumentAttribute Argument
        {
            get { return this.argument; }
        }

        public bool IsMultiple
        {
            get { return this.isMultiple; }
        }

        public Type ElementType
        {
            get 
            {
                if (!this.IsMultiple)
                    return Member.MemberType;
                else
					return Member.MemberType.GetGenericArguments()[0];
            }
        }

        public bool IsFlag(Char c)
        {
            return this.flags.IndexOf(c) > 0;
        }

        public abstract bool Parse(object instance, string arg);

        protected string ExtractValue(string data)
        {
            if (IsFlag(data[0]))
            {
                if (this.Argument.IsDefault)
                    return data;
                else
                    return null;
            }

            int index = data.IndexOf(':');
            if (index < 0)
                return null;
            // check name is correct
            string arg = data.Substring(1, index-1);
            if (arg != this.Argument.LongName &&
                arg != this.Argument.ShortName)
                return null;

            return data.Substring(index+1);
        }

        protected void AssignValue(object instance, object value)
        {
            if (this.IsMultiple)
                this.AddToList(instance, value);
            else
                this.Member.SetValue(instance, value);
        }

        private void AddToList(object instance, object value)
        {
            MethodInfo addMethod = this.Member.MemberType.GetMethod(
                "Add", new Type[] { this.ElementType }
                );
            if (addMethod == null)
                throw new ArgumentException("Could not find add method for field " + this.Member.Name);
            addMethod.Invoke(this.Member.GetValue(instance), new object[] { value });
        }


        public override string ToString()
        {
            return String.Format("P[{0}.{1}]",
                this.Member.DeclaringType.FullName,this.Member.Name
                );
        }

        public virtual void ShowHelp(TextWriter writer)
        {
            if (this.Argument.IsDefault)
            {
                writer.WriteLine("{0} (default)\n\tn: {1}\n\t{2}",
                    this.GetValidValues(),
                    (this.IsMultiple) ? "multiple" : "once",
                    this.Argument.Description
                    );
            }
            else
            {
                writer.WriteLine("/{0}{2}\n/{1}{2}\n\tn: {3}\n\t{4}",
                    this.Argument.LongName,
                    this.Argument.ShortName,
                    this.GetValidValues(),
                    (this.IsMultiple) ? "multiple" : "once",
                    this.Argument.Description
                    );
            }
            writer.WriteLine();
        }

        protected virtual string GetValidValues()
        {
            return String.Format(":<{0}>",this.Member.Name);
        }

        public virtual void ShowData(object instance, TextWriter writer)
        {
            if (this.IsMultiple)
            {
                writer.WriteLine("{0}:", this.Member.Name);
				foreach (object o in this.Member.GetValue(instance) as System.Collections.IEnumerable)
                {
                    writer.WriteLine("\t{0}", o);
                }
            }
            else
                writer.WriteLine("{0}:\t{1}",
                    this.Member.Name,
                    this.Member.GetValue(instance)
                    );
        }
    }
}
