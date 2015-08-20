using System;

namespace QuickGraph.Petri
{
	public interface IMutablePetriNet<Token> : IPetriNet<Token>
	{
        IPlace<Token> AddPlace(string name);
        ITransition<Token> AddTransition(string name);
        IArc<Token> AddArc(IPlace<Token> place, ITransition<Token> transition);
        IArc<Token> AddArc(ITransition<Token> transition, IPlace<Token> place);
    }
}
