using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public interface IStringParser<T>:IParser<T>
	{
		T? Parse(string Line);
	}
}
