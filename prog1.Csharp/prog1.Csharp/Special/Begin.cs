// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
	public class Begin : Special
	{
		public Begin () { }

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
					Console.WriteLine();
					rest.getCar().print(n + 4, false);

					rest = rest.getCdr();
				}
				Console.WriteLine();
				rest.print(n, true);
			}
			else t.getCdr().print(n, true);
		}
	}
}

