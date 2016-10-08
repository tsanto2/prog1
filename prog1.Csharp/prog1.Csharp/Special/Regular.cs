// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
	public class Regular : Special
	{
		public Regular () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
			{
				Console.Write(" ");
			}

			if ( !p )
			{
				Console.Write("(");
			}
			else
			{
				Console.Write(" ");
			}

			Node car = t.getCar();

			if ( !car.isPair() )
			{
				car.print(0, true);
			}
			else
			{
				car.print(n, false);
			}

			if ( t.getCdr().isPair() )
				t.getCdr().print(n, true);
			else if ( t.getCdr().isNull() )
			{
				t.getCdr().print(n, true);
			}
			else
			{
				Console.Write(" . ");
				t.getCdr().print(n, true);
				Console.Write(")");
			}
		}
	}
}


