using System;

namespace QuickGraph.Petri
{
    [Serializable]
    internal sealed class Arc<Token> 
        : Edge<IPetriVertex>
        , IArc<Token>
	{
		private bool isInputArc;
		private IPlace<Token> place;
		private ITransition<Token> transition;
		private IExpression<Token> annotation = new IdentityExpression<Token>();

		public Arc(IPlace<Token> place, ITransition<Token> transition)
            :base(place,transition)
		{
			this.place=place;
			this.transition=transition;
			this.isInputArc=true;
		}
		public Arc(ITransition<Token> transition,IPlace<Token> place)
            :base(place,transition)
        {
            this.place=place;
			this.transition=transition;
			this.isInputArc=false;
		}

		public bool IsInputArc
		{
			get
			{
				return this.isInputArc;
			}
		}

		public IPlace<Token> Place
		{
			get
			{
				return this.place;
			}
		}

		public ITransition<Token> Transition
		{
			get
			{
				return this.transition;
			}
		}

		public IExpression<Token> Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation=value;
			}
		}

		public override string ToString()
		{
			if (this.IsInputArc)
				return String.Format("{0} -> {1}",this.place,this.transition);
			else
				return String.Format("{0} -> {1}",this.transition,this.place);
		}
	}
}
