// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
	public class Quote : Special
	{
		public Quote () { }

		public override void print ( Node t, int n, bool p )
		{
			t.getCar().print(n);

			Node cdr = t.getCdr();

			while ( !cdr.isNull() )
			{
				cdr.getCar().print(0);
				cdr = cdr.getCdr();

				if ( !cdr.isNull() )
					Console.Write(" ");
			}
			if (!t.getCdr().getCar().isPair())
				Console.WriteLine();
		}
	}
}

