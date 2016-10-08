// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
	public class Cond : Special
	{
		public Cond () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
				Console.Write(" ");

			if ( !p )
			{
				Console.Write("(");
			}

			t.getCar().print(0);

			Console.WriteLine();

			Node cdr = t.getCdr();

			if ( cdr.isPair() )
			{
				cdr.getCar().print(n + 4, false);
				if (!cdr.getCar().isPair())
					Console.WriteLine();

				cdr = cdr.getCdr();
				while ( !cdr.isNull() )
				{
					cdr.getCar().print(n + 4, false);
					Console.WriteLine();
					cdr = cdr.getCdr();
				}

				cdr.print(n, true);
			}
			else
				t.getCdr().print(n, true);
		}
	}
}


