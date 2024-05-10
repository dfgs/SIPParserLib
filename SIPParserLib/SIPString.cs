using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public static class SIPString
	{
		public static string? Unescape(string? Line)
		{
			if (Line == null) return null;
			return Regex.Replace(Line, "(%(?<HexValue>[0-9a-fA-F][0-9a-fA-F]))", (match) => ((char)byte.Parse(match.Groups["HexValue"].Value, System.Globalization.NumberStyles.HexNumber)).ToString());
		}
	}
}
