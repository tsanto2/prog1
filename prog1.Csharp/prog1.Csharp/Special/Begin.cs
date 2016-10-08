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

			t.getCar().print(n);

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

