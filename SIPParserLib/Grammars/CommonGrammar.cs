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
												from a in Hex
												from b in Hex
												select (char)((a-48) * 16 + (b-48));
												


		public static ISingleParser<char> UpAlpha = Parse.AnyInRange('A', 'Z');
		public static ISingleParser<char> LowAlpha = Parse.AnyInRange('a', 'z');
		public static ISingleParser<char> Alpha = LowAlpha.Or(UpAlpha);
		public static ISingleParser<char> Alphanum = Alpha.Or(Digit);

		public static ISingleParser<char> Unreserved = Alphanum.Or(Mark);

	}
}
