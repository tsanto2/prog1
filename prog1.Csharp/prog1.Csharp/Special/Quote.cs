// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
	public class Quote : Special
	{
		public Quote () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
			{
				Console.Write(" ");
			}

			t.getCar().print(n);

			Node list = t.getCdr();

			Console.Write("(");

			while ( !list.isNull() )
			{
				list.getCar().print(n);
				list = list.getCdr();

				if ( !list.isNull() )
					Console.Write(" ");
			}

			Nil.getNil().print(n, true);
		}
	}
}

