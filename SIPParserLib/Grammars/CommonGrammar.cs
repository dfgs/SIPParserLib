using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public static class CommonGrammar
	{
		public static ISingleParser<char> Digit = Parse.AnyInRange('0','9');
		public static ISingleParser<char> Mark = Parse.AnyOf('-', '_', '.', '!', '~', '*', '\'', '(', ')');
		public static ISingleParser<char> Hex = Parse.AnyOf('A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f').Or(Digit);
		public static ISingleParser<char> Escaped = from _ in Parse.Char('%')
													from hex in Hex.Then(Hex).ToStringParser()
													select (char)(byte.Parse(hex, System.Globalization.NumberStyles.HexNumber));

		public static ISingleParser<char> QuotedPair = from _ in Parse.Char('\\')
													from value in Parse.Any()
													select value;



		public static ISingleParser<char> UpAlpha = Parse.AnyInRange('A', 'Z');
		public static ISingleParser<char> LowAlpha = Parse.AnyInRange('a', 'z');
		public static ISingleParser<char> Alpha = LowAlpha.Or(UpAlpha);
		public static ISingleParser<char> Alphanum = Alpha.Or(Digit);

		public static ISingleParser<char> Unreserved = Alphanum.Or(Mark);

		public static ISingleParser<string> Token = (Escaped.Or( Parse.Except('(', ')', '<', '>', '@', ',', ';', ':', '\\', '<', '>', '/', '[', ']', '?', '=', '{', '}', '\r', '\n',' '))).ReaderIncludes(' ').OneOrMoreTimes().ToStringParser();
		public static ISingleParser<string> QuotedString = from _ in Parse.Char('"')
														   from value in QuotedPair.Or(Parse.Except('"')).ZeroOrMoreTimes().ReaderIncludes(' ').ToStringParser()
														   from __ in Parse.Char('"')
														   select value;

		public static ISingleParser<char> Separators = Parse.AnyOf('(', ')', '<', '>', '@', ',', ';', ':', '\\' , '<', '>' , '/', '[', ']', '?', '=','{', '}','\r','\n');

	}
}
