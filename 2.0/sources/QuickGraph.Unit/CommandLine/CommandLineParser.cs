using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace QuickGraph.CommandLine
{
    public sealed class CommandLineParser<T> 
        where T : new()
    {
        private ArgumentParserCollection argumentParsers = new ArgumentParserCollection();

        public static CommandLineParser<T> Create()
        {
            CommandLineParser<T> parser = new CommandLineParser<T>();
            if (parser.ArgumentParsers.VerifyParsers())
                return parser;
            else
                return null;
        }

        private CommandLineParser()
        {
            this.ReflectCommandLineType();
        }

        public Type CommandLineType
        {
            get { return typeof(T); }
        }

        internal ArgumentParserCollection ArgumentParsers
        {
            get { return this.argumentParsers; }
        }

        private static ArgumentAttribute GetAttribute(ICustomAttributeProvider t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            // Gets the attributes for the property.
            Object[] attributes =
               t.GetCustomAttributes(typeof(ArgumentAttribute), true);

            if (attributes.Length == 0)
                return null;
            if (attributes.Length == 1)
                return (ArgumentAttribute)attributes[0];

            throw new ArgumentException("Attribute type must be AllowMultiple = false", typeof(T).FullName);
        }

        private void ReflectCommandLineType()
        {
            // get fields
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                ArgumentAttribute attribute = GetAttribute(field);
                if (attribute == null)
                    continue;
                IArgumentParser parser = ArgumentParserFactory.Create(field, attribute);
                if (parser == null)
                    throw new ArgumentException("Could not reflect " + field.Name);
                this.ArgumentParsers.Add(parser);
            }

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				ArgumentAttribute attribute = GetAttribute(property);
				if (attribute == null)
					continue;
				IArgumentParser parser = ArgumentParserFactory.Create(property, attribute);
				if (parser == null)
					throw new ArgumentException("Could not reflect " + property.Name);
				this.ArgumentParsers.Add(parser);
			}
        }

        public void ShowHelp()
        {
            ShowHelp(Console.Out);
        }

        public void ShowHelp(TextWriter writer)
        {
            foreach (IArgumentParser parser in this.ArgumentParsers)
                parser.ShowHelp(writer);
        }

        public bool Parse(T arguments, IEnumerable<string> args)
        {
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            Dictionary<IArgumentParser, object> touchedParsers = new Dictionary<IArgumentParser, object>();
            foreach (string arg in args)
            {
                bool foundparser = false;
                foreach (IArgumentParser parser in this.ArgumentParsers)
                {
                    if (parser.Parse(arguments, arg))
                    {
                        foundparser = true;
                        if (!parser.IsMultiple)
                        {
                            // did we already see this value
                            if (touchedParsers.ContainsKey(parser))
                            {
                                // we found twice a non multiple value
                                throw new ArgumentException("Multiple value found", arg);
                            }
                            else
                            {
                                touchedParsers.Add(parser, null);
                            }
                        }
                        break;
                    }
                }
                if (!foundparser)
                {
                    Console.WriteLine("Could not interpret {0}", arg);
                    return false;
                }
            }

            return true;
        }

        public void ShowData(T arguments)
        {
            ShowData(arguments, Console.Out);
        }

        public void ShowData(T arguments, TextWriter writer)
        {
            if (arguments == null)
                throw new ArgumentNullException("arguments");
            foreach (IArgumentParser parser in this.ArgumentParsers)
                parser.ShowData(arguments, writer);
        }
    }
}
