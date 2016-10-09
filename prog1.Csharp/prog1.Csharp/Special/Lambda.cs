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
			{
				Console.Write("(");
			}

			Console.Write("lambda ");


			t.getCdr().getCar().print(0, false);
			Console.WriteLine();

			Node temp = t.getCdr().getCdr();

			while ( !temp.isNull() )
			{
				if ( !temp.getCar().isPair() )
				{
					temp.getCar().print(n + 4);
					Console.WriteLine();
				}
				else
					temp.getCar().print(n + 4, false);
				temp = temp.getCdr();
			}

			Console.WriteLine();
			temp.print(n, true);
		}
	}
}

