using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	internal static class GroupExtension
	{
		public static string? MatchedValue(this Group Group)
		{
			if (Group.Success) return Group.Value;
			return null;
		}

	}
}
