using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickGraph.CommandLine
{
    internal static class ArgumentParserFactory
    {
		public static IArgumentParser Create(
			PropertyInfo property,
			ArgumentAttribute argument
			)
		{
			bool isMultiple = property.PropertyType.GetGenericArguments().Length != 0;
			if (!isMultiple)
				return CreateFromType(property, argument, property.PropertyType, isMultiple);
			else
				return CreateFromType(property, argument,
					property.PropertyType.GetGenericArguments()[0],
					isMultiple);
		}

		private static IArgumentParser CreateFromType(
			PropertyInfo property,
			ArgumentAttribute argument,
			Type propertyType,
			bool isMultiple
			)
		{
			// is the field type an enum
			if (propertyType.IsEnum)
				return new EnumArgumentParser(new PropertyMember(property), argument, isMultiple);
			if (propertyType == typeof(bool))
				return new BoolArgumentParser(new PropertyMember(property), argument, isMultiple);
			if (propertyType == typeof(string))
				return new StringArgumentParser(new PropertyMember(property), argument, isMultiple);
			if (propertyType == typeof(int))
				return new IntArgumentParser(new PropertyMember(property), argument, isMultiple);
            if (propertyType == typeof(float))
                return new FloatArgumentParser(new PropertyMember(property), argument, isMultiple);
            if (propertyType == typeof(long))
                return new LongArgumentParser(new PropertyMember(property), argument, isMultiple);
            if (propertyType == typeof(DateTime))
                return new DateTimeArgumentParser(new PropertyMember(property), argument, isMultiple);

			Console.WriteLine("Type {0} is not supported", propertyType);
			return null;
		}

        public static IArgumentParser Create(
            FieldInfo field,
            ArgumentAttribute argument
            )
        {
            bool isMultiple = field.FieldType.GetGenericArguments().Length != 0;
            if (!isMultiple)
                return CreateFromType(field, argument, field.FieldType, isMultiple);
            else
                return CreateFromType(field, argument,
                    field.FieldType.GetGenericArguments()[0],
                    isMultiple);
        }

        private static IArgumentParser CreateFromType(
            FieldInfo field,
            ArgumentAttribute argument,
            Type fieldType,
            bool isMultiple
            )
        {
            // is the field type an enum
            if (fieldType.IsEnum)
                return new EnumArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(bool))
				return new BoolArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(string))
				return new StringArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(int))
				return new IntArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(float))
                return new FloatArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(long))
                return new LongArgumentParser(new FieldMember(field), argument, isMultiple);
            if (fieldType == typeof(DateTime))
                return new DateTimeArgumentParser(new FieldMember(field), argument, isMultiple);

			Console.WriteLine("Type {0} is not supported", fieldType);
            return null;
        }
    }
}
