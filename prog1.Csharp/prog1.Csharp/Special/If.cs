// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
	public class If : Special
	{

		public If () { }

		public override void print ( Node t, int n, bool p )
		{
			for ( int i = Console.CursorLeft; i < n; i++ )
				Console.Write(" ");

			if ( !p )
			{
				Console.Write("(");
			}

			Console.Write("if");
            if (t.getCdr().isNull())
                t.getCdr().print(0, true);
            else
            {
                t.getCdr().getCar().print(1, false);
                Console.WriteLine();

                Node temp = t.getCdr().getCdr();

                while (!temp.isNull())
                {
                    if (!temp.getCar().isPair())
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
}
