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

			Node rest = t.getCdr();

			if ( rest.isPair() )
			{
				rest.getCar().print(n + 4, false);

				rest = rest.getCdr();
				while ( !rest.isNull() )
				{
					rest.getCar().print(n + 4, false);

					rest = rest.getCdr();
				}

				Console.Write(")");
			}
			else t.getCdr().print(n, true);
		}
	}
}


