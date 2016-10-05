// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
	public class Cons : Node
	{
		private Node car;
		private Node cdr;
		private Special form;

		public Cons(Node a, Node d)
		{
			car = a;
			cdr = d;
			parseList();
		}

		// parseList() `parses' special forms, constructs an appropriate
		// object of a subclass of Special, and stores a pointer to that
		// object in variable form.  It would be possible to fully parse
		// special forms at this point.  Since this causes complications
		// when using (incorrect) programs as data, it is easiest to let
		// parseList only look at the car for selecting the appropriate
		// object from the Special hierarchy and to leave the rest of
		// parsing up to the interpreter.
		void parseList()
		{
			if ( car.isSymbol() )
			{
				if (car.GetName() == "begin" )
				{
					form = new Begin();
				}

				if (car.GetName() == "cond")
				{
					form = new Cond();
				}

				if ( car.GetName() == "define" )
				{
					form = new Define();
					//Console.Out.WriteLine("Define special form created.");
				}

				if (car.GetName() == "if")
				{
					form = new If();
				}

				if (car.GetName() == "lambda")
				{
					form = new Lambda();
				}

				if (car.GetName() == "let")
				{
					form = new Let();
				}

				if (car.GetName() == "quote")
				{
					form = new Quote();
				}

				if (car.GetName() == "regular")             //Default if (!car.isSymbol())?
				{
					form = new Begin();
				}

				if (car.GetName() == "set")
				{
					form = new Set();
				}

			}
			// TODO: implement this function and any helper functions
			// you might need.
		}

		public override bool isPair ()
		{
			return true;
		}

		public override void print(int n)
		{
			// Added null check for testing
			if ( form != null )
			{
				form.print(this, n, false);
			}
		}

		public override void print(int n, bool p)
		{
			form.print(this, n, p);
		}
	}
}

