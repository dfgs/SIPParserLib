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
		public static IParser<string> Digit = Parse.AnyInRange('0','9');
		public static IParser<string> Mark = Parse.AnyOf('-', '_', '.', '!', '~', '*', '\'', '(', ')');
		public static IParser<string> Hex = Parse.AnyOf('A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f').Or(Digit);
		public static IParser<string> Escaped = Parse.Char('%').Then(Hex).Then(Hex);


		public static IParser<string> UpAlpha = Parse.AnyInRange('A', 'Z');
		public static IParser<string> LowAlpha = Parse.AnyInRange('a', 'z');
		public static IParser<string> Alpha = LowAlpha.Or(UpAlpha);
		public static IParser<string> Alphanum = Alpha.Or(Digit);

		public static IParser<string> Unreserved = Alphanum.Or(Mark);
	}
}
