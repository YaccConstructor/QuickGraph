using System;
using System.Collections.Generic;
using System.IO;

namespace QuickGraph.Petri
{
    [Serializable]
    internal sealed class Place<Token> : IPlace<Token>
    {
		private string name;
		private IList<Token> marking = new List<Token>();

		public Place(string name)
		{
			this.name=name;
		}

		public IList<Token> Marking
		{
			get
			{
				return this.marking;
			}
		}

		public String Name
		{
			get
			{
				return this.name;
			}
		}

		public string ToStringWithMarking()
		{
			StringWriter sw = new StringWriter();
			sw.WriteLine(this.ToString());
			foreach(object token in this.marking)
				sw.WriteLine("\t{0}",token.GetType().Name);

			return sw.ToString();

		}
		public override string ToString()
		{
			return String.Format("P({0}|{1})",this.name,this.marking.Count);
		}
	}
}
