using System;
using System.Collections.Generic;

namespace QuickGraph.Petri
{
    [Serializable]
    public sealed class PetriNetSimulator<Token>
    {
		private IPetriNet<Token> net;
        private Dictionary<ITransition<Token>, TransitionBuffer> transitionBuffers = new Dictionary<ITransition<Token>, TransitionBuffer>();

        public PetriNetSimulator(IPetriNet<Token> net)
		{
            if (net == null)
                throw new ArgumentNullException("net");
            this.net = net;
		}

		public IPetriNet<Token> Net
		{
			get
			{
				return this.net;
			}
		}

		public void Initialize()
		{
			this.transitionBuffers.Clear();
			foreach(ITransition<Token> tr in this.Net.Transitions)
			{
				this.transitionBuffers.Add(tr, new TransitionBuffer());
			}
		}

		public void SimulateStep()
		{
			// first step, iterate over arc and gather tokens in transitions
			foreach(IArc<Token> arc in this.Net.Arcs)
			{
				if(!arc.IsInputArc)
					continue;

				IList<Token> tokens = this.transitionBuffers[arc.Transition].Tokens;
				// get annotated tokens
                IList<Token> annotatedTokens = arc.Annotation.Eval(arc.Place.Marking);
                //add annontated tokens
                foreach(Token annotatedToken in annotatedTokens)
                    tokens.Add(annotatedToken);
            }

			// second step, see which transition was enabled
			foreach(ITransition<Token> tr in this.Net.Transitions)
			{
				// get buffered tokens
                IList<Token> tokens = this.transitionBuffers[tr].Tokens;
                // check if enabled, store value
                this.transitionBuffers[tr].Enabled = tr.Condition.IsEnabled(tokens);
            }

			// third step, iterate over the arcs
			foreach(IArc<Token> arc in this.Net.Arcs)
			{
				if (!this.transitionBuffers[arc.Transition].Enabled)
					continue;

				if(arc.IsInputArc)
				{
					// get annotated tokens
                    IList<Token> annotatedTokens = arc.Annotation.Eval(arc.Place.Marking);
                    // remove annotated comments from source place
                    foreach(Token annotatedToken in annotatedTokens)
                        arc.Place.Marking.Remove(annotatedToken);
                }
				else
				{
                    IList<Token> tokens = this.transitionBuffers[arc.Transition].Tokens;
                    // get annotated tokens
                    IList<Token> annotatedTokens = arc.Annotation.Eval(tokens);
                    // IList<Token> annotated comments to target place
                    foreach(Token annotatedToken in annotatedTokens)
                        arc.Place.Marking.Add(annotatedToken);
                }
			}
			// step four, clear buffers
			foreach(ITransition<Token> tr in this.Net.Transitions)
			{
				this.transitionBuffers[tr].Tokens.Clear();
                this.transitionBuffers[tr].Enabled = false;
            }
		}

        private sealed class TransitionBuffer
        {
            private IList<Token> tokens = new List<Token>();
            private bool enabled = true;

            public IList<Token> Tokens
            {
                get { return this.tokens;}
            }
            public bool Enabled
            {
                get { return this.enabled; }
                set { this.enabled = value; }
            }
        }
    }
}
