using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.CommandLine
{
	internal interface IMember
	{
		string Name { get;}
		Type DeclaringType { get;}
		Type MemberType { get;}
		object GetValue(object instance);
		void SetValue(object instance, object value);
	}
}
