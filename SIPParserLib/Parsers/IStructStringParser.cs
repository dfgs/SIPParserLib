using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public interface IStructStringParser<T>:IParser<T>
		where T:struct
	{
		bool Parse(Group? Group, out T? Value, bool IsMandatory);
		bool Parse(string? Line, out T? Value, bool IsMandatory);

		bool ParseAll(Group? Group,char Separator,  out T[]? Value, bool IsMandatory);
		bool ParseAll(string? Line, char Separator, out T[]? Value, bool IsMandatory);


	}
}
