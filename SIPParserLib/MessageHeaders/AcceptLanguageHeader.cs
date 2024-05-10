using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AcceptLanguageHeader:MessageHeader<string>
	{
		public override string Name => "Accept-Language";
		public AcceptLanguageHeader(string? Value):base(Value)
		{
		}

	}
}
