// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        // TODO: Add any fields needed.
    
        // TODO: Add an appropriate constructor.
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
			if ( !p )
			{
				Console.Write("(");
			}
			if ( t.getCar().isSymbol() || t.getCar().isNumber() || t.getCar().isBool() || t.getCar().isString() )
			{
				t.getCar().print(1);
			}
			else if ( t.getCar().isPair() )
			{
				t.getCar().print(0);
			}

			if ( t.getCdr().isPair() )
			{
				t.getCdr().print(1, true);
				if ( !t.getCdr().getCar().isPair() )
					Console.Write(")");
			}
        }
    }
}


