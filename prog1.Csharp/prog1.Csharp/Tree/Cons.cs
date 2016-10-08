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
			form = new Regular();
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
				else if (car.GetName() == "cond")
				{
					form = new Cond();
				}
				else if ( car.GetName() == "define" )
				{
					form = new Define();
					Console.Out.WriteLine("Define special form created.");
				}
				else if ( car.GetName() == "if")
				{
					form = new If();
				}
				else if ( car.GetName() == "lambda")
				{
					form = new Lambda();
				}
				else if ( car.GetName() == "let")
				{
					form = new Let();
				}
				else if ( car.GetName() == "quote")
				{
					form = new Quote();
				}
				else if ( car.GetName() == "set!")
				{
					form = new Set();
				}
				else
				{
					form = new Regular();
				}

			}
			// TODO: implement this function and any helper functions
			// you might need.
		}

		public override Node getCar ()
		{
			return car;
		}

		public override Node getCdr ()
		{
			return cdr;
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
				Console.WriteLine();
			}
		}

		public override void print(int n, bool p)
		{
			form.print(this, n, p);
		}
	}
}

