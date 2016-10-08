// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
	public class Set : Special
	{
		public Set () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
				Console.Write(" ");


			if ( !p )
			{
				Console.Write("(");
			}

			//t.getCar().print(n);
			Console.Write("set!");

			if ( t.getCar().isPair() )
			{
				t.getCar().print(n + 4, true);
			}
			else t.getCdr().print(0, true);
		}
	}
}
