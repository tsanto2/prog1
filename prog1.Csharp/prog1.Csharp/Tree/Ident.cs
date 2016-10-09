// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
	public class Ident : Node
	{
		private string name;

		public Ident(string n)
		{
			name = n;
		}

		public override string GetName ()
		{
			return name;
		}

		public override bool isSymbol () { return true; }

		public override void print(int n)
		{
		// There got to be a more efficient way to print n spaces.
			for (int i = 0; i < n; i++)
				Console.Write(" ");
			if ( name == "quote" )
				Console.Write("'");
			else
				Console.Write(name);
		}
	}
}

