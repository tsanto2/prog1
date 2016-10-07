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
	public class Parser
	{

		#region Variables

		private Scanner scanner;
		private bool tokensRead;

		private const int BUFSIZE = 1000;
		private int i;
		public Token[] tokens;

		#endregion

		public Parser ( Scanner s ) {
			scanner = s;
			tokens = new Token[BUFSIZE];
		}

		public Node parseExp ()
		{
			// Read all tokens of expression into our buffer array
			if ( !tokensRead )
				ReadTokens();

			TokenType tt = tokens[i].getType();

			// If we have left parenthesis, go to next token, parse rest
			if ( tt == TokenType.LPAREN )
			{
				//print("(");
				i++;

				return parseRest();
			}
			// If our token is an identifier...
			else if (tt == TokenType.IDENT )
			{
				//print(tokens[i].getName());
				Ident newIdent = new Ident(tokens[i].getName());
				i++;

				return newIdent;
			}
			// If our token is an integer...
			else if ( tt == TokenType.INT )
			{
				//print(tokens[i].getIntVal().ToString());
				IntLit newInt = new IntLit(tokens[i].getIntVal());
				i++;

				return newInt;
			}
			// If our token is a string...
			else if ( tt == TokenType.STRING )
			{
				//print(tokens[i].getStringVal());
				StringLit newStr = new StringLit(tokens[i].getStringVal());
				i++;

				return newStr;
			}
			// If our token is a true bool...
			else if ( tt == TokenType.TRUE )
			{
				//print("#t");
				BoolLit newBool = new BoolLit(true);
				i++;

				return newBool;
			}
			// If our token is a false bool...
			else if ( tt == TokenType.FALSE )
			{
				//print("#f");
				BoolLit newBool = new BoolLit(false);
				i++;

				return newBool;
			}
			// If our token is a QUOTE...
			else if ( tt == TokenType.QUOTE )
			{
				//print("\'");

			}

			return null;
		}

		protected Node parseRest ()
		{
			Node car, cdr;
			Cons newCons;

			TokenType tt = tokens[i].getType();

			if ( tt != TokenType.RPAREN )
			{
				car = parseExp();
				cdr = parseCdr();
				newCons = new Cons(car, cdr);

				return newCons;
			}
			else
			{
				//print(")");
				i++;

				return new Nil();
			}
		}

		Node parseCdr ()
		{
			TokenType tt = tokens[i].getType();

			// If we have adot, cdr is next token, not a nil
			if (tt == TokenType.DOT )
			{
				i++;
				//print(".");

				return parseExp();
			}

			return parseRest();
		}

		// This method reads all tokens from the scanner into the buffer array
		void ReadTokens ()
		{
			Token tok;

			do
			{
				tok = scanner.getNextToken();

				if ( tok != null )
				{
					tokens[i] = tok;
					i++;
				}
			} while ( tok != null );

			i = 0;
			tokensRead = true;
		}

		// Convenient printing method
		void print(string str )
		{
			Console.Out.WriteLine(str);
		}
	}
}