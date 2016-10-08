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

			t.getCar().print(0);

			Node rest = t.getCdr();

			if ( rest.getCar().isPair() )
			{
				Console.Write(" ");
				rest.getCar().print(n + 4, false);

				if ( rest.getCdr().getCar() != null )
				{
					if ( rest.getCdr().getCar().isPair() )
					{
						for ( int i = Console.CursorLeft; i < n + 4; i++ )
							Console.Write(" ");
					}
				}

				rest = rest.getCdr();
				while ( !rest.isNull() )
				{
					rest.getCar().print(n + 4, false);

					rest = rest.getCdr();
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


