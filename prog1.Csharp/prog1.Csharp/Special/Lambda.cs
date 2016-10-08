// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
	public class Lambda : Special
	{

		public Lambda () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
				Console.Write(" ");

			if ( !p )
				Console.Write("(");

			t.getCar().print(n);

			Console.Write(" ");

			Node second = t.getCdr().getCar();
			if ( second.isPair() )
			{
				second.print(n, false);
			}

			Console.WriteLine();

			Node term = t.getCdr().getCdr();

			while ( !term.isNull() )
			{
				term.getCar().print(n + 4);
				term = term.getCdr();
				Console.WriteLine();
			}

			Nil.getNil().print(n, true);
		}
	}
}

