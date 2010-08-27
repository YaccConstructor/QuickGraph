using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
	internal sealed class FieldMember : IMember
	{
		private FieldInfo field;
		public FieldMember(FieldInfo field)
		{
			this.field = field;
		}

		public string Name
		{
			get { return this.field.Name; }
		}

		public Type DeclaringType
		{
			get { return this.field.DeclaringType; }
		}

		public Type MemberType
		{
			get { return this.field.FieldType; }
		}

		public object GetValue(object instance)
		{
			return this.field.GetValue(instance);
		}

		public void SetValue(object instance, object value)
		{
			this.field.SetValue(instance, value);
		}
	}
}
