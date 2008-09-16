using System;
using System.Collections.Generic;
using System.IO;

namespace QuickGraph.Petri
{
    [Serializable]
    public sealed class PetriNet<Token> : IMutablePetriNet<Token>
    {
        private List<IPlace<Token>> places = new List<IPlace<Token>>();
        private List<ITransition<Token>> transitions = new List<ITransition<Token>>();
        private List<IArc<Token>> arcs = new List<IArc<Token>>();
        private PetriGraph<Token> graph = new PetriGraph<Token>();      

		public PetriNet()
		{}

		public IPetriGraph<Token> Graph
		{
			get
			{
				return this.graph;
			}
		}

		public IPlace<Token> AddPlace(string name)
		{
			IPlace<Token> p = new Place<Token>(name);
			this.places.Add(p);
			this.graph.AddVertex(p);
			return p;
		}
		public ITransition<Token> AddTransition(string name)
		{
			ITransition<Token> tr = new Transition<Token>(name);
			this.transitions.Add(tr);
			this.graph.AddVertex(tr);
			return tr;
		}
		public IArc<Token> AddArc(IPlace<Token> place, ITransition<Token> transition)
		{
            IArc<Token> arc = new Arc<Token>(place, transition);
            this.arcs.Add(arc);
			this.graph.AddEdge(arc);
			return arc;
		}
		public IArc<Token> AddArc(ITransition<Token> transition,IPlace<Token> place)
		{
			IArc<Token> arc=new Arc<Token>(transition,place);
			this.arcs.Add(arc);
			this.graph.AddEdge(arc);
			return arc;
		}

		public IList<IPlace<Token>> Places
		{
			get
			{
				return this.places;
			}
		}

		public IList<ITransition<Token>> Transitions
		{
			get
			{
				return this.transitions;
			}
		}

		public IList<IArc<Token>> Arcs
		{
			get
			{
				return this.arcs;
			}
		}

		public override string ToString()
		{
			StringWriter sw = new StringWriter();
			sw.WriteLine("-----------------------------------------------");
			sw.WriteLine("Places ({0})",this.places.Count);
            foreach (IPlace<Token> place in this.places)
            {
				sw.WriteLine("\t{0}",place.ToStringWithMarking());
			}

			sw.WriteLine("Transitions ({0})",this.transitions.Count);
            foreach (ITransition<Token> transition in this.transitions)
            {
				sw.WriteLine("\t{0}",transition);
			}

			sw.WriteLine("Arcs");
            foreach (IArc<Token> arc in this.arcs)
            {
				sw.WriteLine("\t{0}",arc);
			}
			return sw.ToString();
		}

	}
}
