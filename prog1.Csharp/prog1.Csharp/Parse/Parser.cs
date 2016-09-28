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
		Token[] tokens;

		public Node parseExp()
		{
			Token tok = scanner.getNextToken();
			if ( tok != null )
			{
				TokenType tt = tok.getType();
			}

			if (tok != null )
			{
				tokens[i] = tok;
				//Console.Out.WriteLine(tokens[i].getIntVal());
				i++;
				parseExp();
			}

			for (int x=0; x < tokens.Length; x++ )
			{
				if(tokens[x]!=null)
					Console.Out.WriteLine(tokens[x].getName());
			}

			return null;
			
			// TODO: write code for parsing an exp
			/*Token tok = scanner.getNextToken();
			TokenType tt = tok.getType();

			if ( tt == TokenType.LPAREN )
			{
				//Cons newCons = new Tree.Cons(tok., new Nil());
				Console.Out.WriteLine("yolo");
				//return newCons;
			}
			if ( tt == TokenType.IDENT )
			{
				Ident identifier = new Ident(tok.getName());
				//Console.Out.WriteLine("****PRINTING IDENT****");
				return identifier;
			}
			else if ( tt == TokenType.INT )
			{
				IntLit integer = new IntLit(tok.getIntVal());
				//Console.Out.WriteLine("****PRINTING INT****");
				return integer;
			}
			else if ( tt == TokenType.TRUE )
			{
				BoolLit boolean = new BoolLit(true);
				//Console.Out.WriteLine("****PRINTING BOOL****");
				return boolean;
			}
			else if ( tt == TokenType.FALSE )
			{
				BoolLit boolean = new BoolLit(false);
				//Console.Out.WriteLine("****PRINTING BOOL****");
				return boolean;
			}
			else if ( tt == TokenType.STRING )
			{
				StringLit str = new StringLit(tok.getStringVal());
				//Console.Out.WriteLine("****PRINTING STRING****");
				return str;
			}
			else
				return null;
				*/
				
		}

		protected Node parseRest()
        {
            // TODO: write code for parsing a rest
            return null;
        }

        // TODO: Add any additional methods you might need.
    }
}

