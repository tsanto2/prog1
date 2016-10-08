// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
	public class Define : Special
	{

		public Define () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
				Console.Write(" ");

			if ( !p )
			{
				Console.Write("(");
			}

			t.getCar().print(n);

			Node rest = t.getCdr();

			if ( rest.getCar().isPair() )
			{
				Console.Write(" ");
				rest.getCar().print(n, false);
				Console.WriteLine();

				if ( rest.getCdr().getCar().isPair() )
				{
					for ( int i = Console.CursorLeft; i < n + 4; i++ )
						Console.Write(" ");
					Console.Write("(");
				}

				while ( (rest = rest.getCdr()) != Nil.getNil() )
				{
					rest.getCar().print(n + 4, true);
					Console.WriteLine();
				}
				Console.Write(")");
			}
			else
			{
				t.getCdr().print(n, true);
			}
		}
	}
}


