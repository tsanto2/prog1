// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];
		int length = 0;
		int lParenCount, rParenCount;

		public Scanner(TextReader i) { In = i;
			Console.Out.WriteLine("****SCANNER START****");
		}
  
        // TODO: Add any other methods you need

		void checkDone (int ch)
		{
			if ( In.Peek() == 13 && ch != ')' )
			{
				In.Read();
				getNextToken();
			}
		}

        public Token getNextToken()
        {
            int ch, nextch;

			Array.Clear(buf, 0, length);
			length = 0;

			try
            {
				// It would be more efficient if we'd maintain our own
				// input buffer and read characters out of that
				// buffer, but reading individual characters from the
				// input stream is easier.


				ch = In.Read();
				nextch = In.Peek();
				//Console.Out.WriteLine(In.Peek());

				// TODO: skip white space and comments

				if ( ch == -1 )
				{
					return null;
				}

				else if ( (char) ch == 10 || (char) ch == 13 || (char) ch == 32 || (char) ch == 9 )
				{
					if ( lParenCount != rParenCount || (lParenCount == 0) )
						return getNextToken();
					else
						return null;
				}

				// Special characters
				else if ( ch == '\'' )
					return new Token(TokenType.QUOTE);
				else if ( ch == '(' )
				{
					lParenCount++;
					return new Token(TokenType.LPAREN);
				}
				else if ( ch == ')' )
				{
					rParenCount++;
					return new Token(TokenType.RPAREN);
				}
				else if ( ch == '.' )
					// We ignore the special identifier `...'.
					return new Token(TokenType.DOT);

				// Boolean constants
				else if ( ch == '#' )
				{
					ch = In.Read();

					if ( ch == 't' )
						return new Token(TokenType.TRUE);
					else if ( ch == 'f' )
						return new Token(TokenType.FALSE);
					else if ( ch == -1 )
					{
						Console.Error.WriteLine("Unexpected EOF following #");
						return null;
					}
					else
					{
						Console.Error.WriteLine("Illegal character '" +
												(char) ch + "' following #");
						return getNextToken();
					}
				}

				// String constants
				else if ( ch == '"' )
				{
					// TODO: scan a string into the buffer variable buf

					while ( In.Peek() != 13 && In.Peek() != 32 && In.Peek() != 34 && In.Peek() != 40 && In.Peek() != 41 && length < BUFSIZE )
					{
						buf[length] = (char) In.Read();
						length++;
					}

					if ( In.Peek() == 34 )
						In.Read();

					return new StringToken(new String(buf, 0, length));
				}


				// Integer constants
				else if ( ch >= '0' && ch <= '9' )
				{
					buf[length] = (char) ch;
					length++;

					while ( In.Peek() >= '0' && In.Peek() <= '9' )
					{
						buf[length] = (char) In.Read();
						length++;
					}

					string tempNumStr = "";
					for ( int x = 0; x <= length; x++ )
					{
						tempNumStr += buf[x];
					}

					int i = Convert.ToInt32(tempNumStr);
					//int i = ch - '0';

					// TODO: scan the number and convert it to an integer

					// make sure that the character following the integer
					// is not removed from the input stream
					return new IntToken(i);
				}

				// Identifiers
				else if ( (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')
						 // or ch is some other valid first character
						 // for an identifier
						 )
				{
					// TODO: scan an identifier into the buffer
					buf[length] = (char) ch;
					length++;

					while ( In.Peek() != 13 && In.Peek() != 32 && In.Peek() != 40 && In.Peek() != 41 && length < BUFSIZE )
					{
						// Console.Out.WriteLine(length);
						buf[length] = (char) In.Read();
						length++;
					}

					// make sure that the character following the integer
					// is not removed from the input stream
					char[] tempBuf = buf;
					int tempLength = length;
					//checkDone(ch);

					string tempString = new String(tempBuf, 0, tempLength);


					return new IdentToken(tempString);
				}

				// Illegal character
				else
				{
					Console.Error.WriteLine("Illegal input character '"
											+ (char) ch + '\'');
					return getNextToken();
				}
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }

		public void ResetValues ()
		{
			lParenCount = 0;
			rParenCount = 0;
		}
    }

}

