// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
	
        private Scanner scanner;

        public Parser(Scanner s) {
			scanner = s;
			tokens = new Token[BUFSIZE];
		}

		private const int BUFSIZE = 1000;
		private int i = 0;
		private int x = 1;
		public Token[] tokens;

		private int LParenCount, RParenCount;

		public Node parseExp()
		{
			Token tok = null;
			
			Array.Clear(tokens, 0, i);

			do
			{
				tok = scanner.getNextToken();
				if ( tok != null )
				{
					TokenType tt = tok.getType();
					tokens[i] = tok;
					i++;
				}
			} while ( tok != null );

			if ( tokens[0] != null )
			{
				TokenType tt = tokens[0].getType();

				if (tt == TokenType.LPAREN )
				{
					++LParenCount;
					print("(");
					if ( tokens[1] != null )
					{
						Cons newCons;

						if ( tokens[1].getType() == TokenType.IDENT )
						{
							Ident newIdent = new Ident(tokens[1].getName());
							print(tokens[1].getName());
							print(" ");
							newCons = new Cons(newIdent, parseRest());
						}
						else if ( tokens[1].getType() == TokenType.INT )
						{
							IntLit newInt = new IntLit(tokens[1].getIntVal());
							print(tokens[1].getIntVal().ToString());
							print(" ");
							newCons = new Cons(newInt, parseRest());
						}
						else if ( tokens[1].getType() == TokenType.STRING )
						{
							StringLit newString = new StringLit(tokens[1].getStringVal());
							print("'" + tokens[1].getStringVal());
							print("' ");
							newCons = new Cons(newString, parseRest());
						}
						else if ( tokens[1].getType() == TokenType.LPAREN )
						{
							++LParenCount;
							print("(");
							--x;
							
							newCons = new Cons(GetCar(), parseRest());

							return newCons;
						}
						else
						{
							newCons = null;
						}

						i = 0;
						x = 1;
						scanner.ResetValues();
						return newCons;
					}
				}
			}

			return null;
		}

		protected Node parseRest()
        {
			Cons newCons;

            // TODO: write code for parsing a rest
			if ( tokens[x+1].getType() == TokenType.IDENT )
			{
				Ident newIdent = new Ident(tokens[x + 1].getName());
				x++;
				print(tokens[x].getName());
				if (tokens[x+1].getType() != TokenType.RPAREN)
					print(" ");
				newCons = new Cons(newIdent, parseRest());
				return newCons;
			}
			else if ( tokens[x + 1].getType() == TokenType.INT )
			{
				IntLit newInt = new IntLit(tokens[x + 1].getIntVal());
				x++;
				print(tokens[x].getIntVal().ToString());
				if ( tokens[x + 1].getType() != TokenType.RPAREN )
					print(" ");
				newCons = new Cons(newInt, parseRest());
				return newCons;
			}
			else if ( tokens[x + 1].getType() == TokenType.STRING )
			{
				StringLit newString = new StringLit(tokens[x + 1].getStringVal());
				x++;
				print("'" + tokens[x].getStringVal() + "'");
				if ( tokens[x + 1].getType() != TokenType.RPAREN )
					print(" ");
				newCons = new Cons(newString, parseRest());
				return newCons;
			}
			else if ( tokens[x + 1].getType() == TokenType.TRUE || tokens[x + 1].getType() == TokenType.FALSE )
			{
				TokenType tt = tokens[x + 1].getType();
				BoolLit newBool;

				if ( tt == TokenType.TRUE ) {
					newBool = new BoolLit(true);
					print("#t");
				}
				else
				{
					newBool = new BoolLit(false);
					print("#f");
				}

				x++;

				if ( tokens[x + 1].getType() != TokenType.RPAREN )
					print(" ");
				newCons = new Cons(newBool, parseRest());
				return newCons;
			}
			else if (tokens[x+1].getType() == TokenType.LPAREN)
			{
				++LParenCount;
				print("(");

				newCons = new Cons(GetCar(), parseRest());
				return newCons;
			}

			else if (tokens[x+1].getType() == TokenType.RPAREN)
			{
				++RParenCount;
				if ( RParenCount == LParenCount )
				{
					print("\n");
					print(")\n");
					return new Nil();
				}
				else
				{
					x++;
					print(") ");
					return parseRest();
				}
			}
			else
			{
				newCons = null;
				return newCons;
			}
        }

		Node GetCar ()
		{
			++x;

			if ( tokens[x + 1].getType() == TokenType.IDENT )
			{
				Ident newIdent = new Ident(tokens[x + 1].getName());
				print(tokens[x].getName());
				return newIdent;
			}
			else
			{
				return new Nil();
			}
		}

        // TODO: Add any additional methods you might need.

		// To make printing easier...
		void print(String str)
		{
			Console.Out.Write(str);
		}
    }
}
